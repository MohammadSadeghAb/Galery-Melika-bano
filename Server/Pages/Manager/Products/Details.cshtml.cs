using Application.ProductApp;
using Domain.Users;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Products;

namespace Server.Pages.Manager.Products;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Manager)]

public class DetailsModel : BasePageModel
{
    private readonly IProductApplication _application;

    public DetailsModel(IProductApplication application)
    {
        _application = application;
        ViewModel = new();
    }

    public DetailsViewModel ViewModel { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError
                (message: Resources.Messages.Errors.IdIsNull);

            return RedirectToPage(pageName: "Index");
        }

        ViewModel = (await _application.GetProduct(id.Value)).Data;

        if (ViewModel == null)
        {
            AddToastError
                (message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage(pageName: "Index");
        }

        return Page();
    }
}
