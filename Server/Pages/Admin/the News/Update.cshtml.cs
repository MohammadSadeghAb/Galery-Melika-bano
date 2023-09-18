using Application.NewsApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Resources;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.NEWS;

namespace Server.Pages.Admin.the_News;

[Authorize(Roles = Infrastructure.Constants.Role.Admin)]

public class UpdateModel : BasePageModel
{
    private readonly INewsApplication _application;

    public UpdateModel(INewsApplication application)
    {
        _application = application;
        ViewModel = new();
    }

    [BindProperty]
    public CommonViewModel ViewModel { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError(Resources.Messages.Errors.IdIsNull);

            return RedirectToPage("Index");
        }

        ViewModel = (await _application.GetNews(id.Value)).Data;

        if (ViewModel == null)
        {
            AddToastError(Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage("Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        var res = await _application.UpdateNews(ViewModel);

        if (res.Succeeded == false || res.ErrorMessages.Count > 0)
        {
            foreach (var item in res.ErrorMessages)
            {
                AddToastError(item);
            }

            return Page();
        }

        var successMessage = string.Format(Resources.Messages.Successes.Updated, DataDictionary.Thenews);

        AddToastSuccess(successMessage);

        return RedirectToPage("Index");
    }
}
