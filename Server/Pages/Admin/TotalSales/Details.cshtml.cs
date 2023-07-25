using Application.ProductApp;
using Application.TotalSaleApp;
using Application.UserApp;
using Framework.OperationResult;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using ViewModels.Pages.Admin.TotalSales;

namespace Server.Pages.Admin.TotalSales;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Constants.Role.Admin)]

public class DetailsModel : BasePageModel
{
    private readonly ITotalSaleApplication _application;

    private readonly IUserApplication _user;

    private readonly IProductApplication _product;

    public DetailsModel(ITotalSaleApplication application, IUserApplication user, IProductApplication product)
    {
        _user = user;
        _product = product;
        _application = application;
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

        ViewModel = (await _application.GetTotalSale(id.Value)).Data;

        if (ViewModel == null)
        {
            AddToastError
                (message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage(pageName: "Index");
        }

        User = await _user.GetUser(ViewModel.UserId);

        Product = await _product.GetProduct(ViewModel.Products);

        return Page();
    }
}
