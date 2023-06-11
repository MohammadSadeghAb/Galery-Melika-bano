using Application.ProductApp;
using Application.SaleApp;
using Application.UserApp;
using Domain.CategoryAgg;
using Framework.OperationResult;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Sales;

namespace Server.Pages.Admin.Sales;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class DetailsModel : BasePageModel
{
    private readonly ISaleApplication _application;

    private readonly IUserApplication _user;

    private readonly IProductApplication _product;

    public DetailsModel(ISaleApplication application,
                        IUserApplication user,
                        IProductApplication product)
    {
        _user = user;
        _product = product;
        _application = application;
        ViewModel = new();
    }

    public DetailsViewModel ViewModel { get; private set; }

    public OperationResultWithData<ViewModels.Pages.Admin.Users.DetailsViewModel> User { get; set; }
    public OperationResultWithData<ViewModels.Pages.Admin.Products.DetailsViewModel> Product { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError
                (message: Resources.Messages.Errors.IdIsNull);

            return RedirectToPage(pageName: "Index");
        }

        ViewModel = (await _application.GetSale(id.Value)).Data;

        ViewModel.Price_Total = ViewModel.Price * ViewModel.Number;

        if (ViewModel == null)
        {
            AddToastError
                (message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage(pageName: "Index");
        }

        User = await _user.GetUser(ViewModel.UserId);

        Product = await _product.GetProduct(ViewModel.ProductId);

        return Page();
    }
}
