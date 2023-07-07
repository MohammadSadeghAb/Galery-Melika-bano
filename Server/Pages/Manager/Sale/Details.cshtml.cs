using Application.ProductApp;
using Application.TotalSaleApp;
using Application.UserApp;
using Framework.OperationResult;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.TotalSales;
using static System.Net.Mime.MediaTypeNames;

namespace Server.Pages.Manager.Sale;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Manager)]

public class DetailsModel : BasePageModel
{
    private readonly ITotalSaleApplication _totalsale;

    private readonly IUserApplication _user;

    private readonly IProductApplication _product;

    public DetailsModel(IProductApplication product,
                        IUserApplication user,
                        ITotalSaleApplication totalsale)
    {
        _user = user;
        _product = product;
        _totalsale = totalsale;
        ViewModelTotalSale = new();
    }

    public DetailsViewModel ViewModelTotalSale { get; set; }

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

        ViewModelTotalSale = (await _totalsale.GetTotalSale(id.Value)).Data;

        if (ViewModelTotalSale == null)
        {
            AddToastError
                (message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage(pageName: "Index");
        }

        User = await _user.GetUser(ViewModelTotalSale.UserId);

        Product = await _product.GetProduct(ViewModelTotalSale.Products);

        return Page();
    }
}
