using Application.ProductApp;
using Application.SaleApp;
using Application.UserApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Resources;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Sales;

namespace Server.Pages.Admin.Sales;

[Authorize(Roles = Infrastructure.Constants.Role.Admin)]

public class UpdateModel : BasePageModel
{
    private readonly ISaleApplication _application;

    public UpdateModel(ISaleApplication application)
    {
        _application = application;
        ViewModel = new();
    }

    [BindProperty]
    public UpdateViewMode ViewModel { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError(Resources.Messages.Errors.IdIsNull);

            return RedirectToPage("Index");
        }

        ViewModel = (await _application.GetSale(id.Value)).Data;

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

        var res = await _application.UpdateSale(ViewModel);

        if (res.Succeeded == false || res.ErrorMessages.Count > 0)
        {
            foreach (var item in res.ErrorMessages)
            {
                AddToastError(item);
            }

            return Page();
        }

        var successMessage = string.Format(Resources.Messages.Successes.Updated, DataDictionary.Sale);

        AddToastSuccess(successMessage);

        return RedirectToPage("Index");
    }
}
