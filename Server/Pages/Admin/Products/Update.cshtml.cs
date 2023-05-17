using Application.ProductApp;
using Application.UserApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resources;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Products;

namespace Server.Pages.Admin.Products;

[Authorize(Roles = Infrastructure.Constants.Role.Admin)]

public class UpdateModel : Infrastructure.BasePageModel
{
    public UpdateModel(IProductApplication application)
    {
        _application = application;
        ViewModel = new();
    }

    private readonly IProductApplication _application;

    [BindProperty]
    public UpdateViewModel ViewModel { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError(Resources.Messages.Errors.IdIsNull);

            return RedirectToPage("Index");
        }

        ViewModel = (await _application.GetProduct(id.Value)).Data;

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

        var res = await _application.UpdateProduct(ViewModel);

        if (res.Succeeded == false || res.ErrorMessages.Count > 0)
        {
            foreach (var item in res.ErrorMessages)
            {
                AddToastError(item);
            }

            return Page();
        }

        var successMessage = string.Format(Resources.Messages.Successes.Updated, DataDictionary.Product);

        AddToastSuccess(successMessage);

        return RedirectToPage("Index");
    }
}
