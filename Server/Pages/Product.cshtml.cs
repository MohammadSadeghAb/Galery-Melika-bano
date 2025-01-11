using Application.ProductApp;
using Application.SaleApp;
using Domain.CategoryAgg;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Products;
using static System.Net.Mime.MediaTypeNames;

namespace Server.Pages;

public class ProductModel : BasePageModel
{
    private readonly IProductApplication _product;

    private readonly ISaleApplication _sale;

    public readonly DatabaseContext _context;

    public ProductModel(IProductApplication product,
                        ISaleApplication sale,
                        DatabaseContext context)
    {
        _sale = sale;
        _product = product;
        _context = context;
        ViewModel = new();
        ViewModelSale = new();
        ViewModelUpdate = new();
    }

    public DetailsViewModel ViewModel { get; set; }

    public ViewModels.Pages.Admin.Sales.UpdateViewMode ViewModelUpdate { get; set; }

    public ViewModels.Pages.Admin.Sales.CreateViewModel ViewModelSale { get; set; }

    [BindProperty]
    public int Number { get; set; }

    [BindProperty]
    public string Color { get; set; }

    public Category category { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError
                (message: Resources.Messages.Errors.IdIsNull);

            return RedirectToPage(pageName: "Index");
        }

        ViewModel = (await _product.GetProduct(id.Value)).Data;

        category = _context.Categories.FirstOrDefault(x => x.Id == ViewModel.CategoryParent_Id);

        if (ViewModel == null)
        {
            AddToastError
                (message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);
            return RedirectToPage(pageName: "Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError
                (message: Resources.Messages.Errors.IdIsNull);

            return RedirectToPage(pageName: "Index");
        }

        ViewModel = (await _product.GetProduct(id.Value)).Data;

        category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == ViewModel.CategoryParent_Id);

        if (User == null ||
            User.Identity == null ||
            User.Identity.IsAuthenticated == false)
        {
            AddToastError(message: Resources.Messages.Errors.Pleaseregister_loginfirst);
            return Page();
        }
        else
        {
            if (Number == 0)
            {
                AddToastWarning(Resources.Messages.Errors.Firstselectthedesirednumber);
                return Page();
            }

            if (Number > 0)
            {
                if (Number > ViewModel.Number)
                {
                    AddToastError(message: Resources.Messages.Errors.Thisnumberofproductsisnotavailable);
                    return Page();
                }
                if (Number <= ViewModel.Number)
                {
                    Guid userid = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

                    var checksale = await _context.Sales.FirstOrDefaultAsync(x => x.UserId == userid && x.ProductId == id.Value);
                    
                    if (category.Name == Resources.DataDictionary.Free)
                    {
                        var checksalefree = await _context.Sales.FirstOrDefaultAsync(x => x.UserId == userid && x.ProductId == id.Value);
                        var checktotalsalefree = await _context.Sales.FirstOrDefaultAsync(x => x.UserId == userid && x.ProductId == id.Value);

                        if (checksalefree != null && checksalefree.Number >= 5 && checktotalsalefree != null && checktotalsalefree.Number >= 5)
                        {
                            AddToastError(message: Resources.Messages.Errors.Thisnumberofproductsisnotavailable);
                            return Page();
                        }
                    }

                    if (checksale != null)
                    {
                        if (User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "Special")
                        {
                            ViewModelUpdate.Id = checksale.Id;
                            ViewModelUpdate.Number = checksale.Number + Number;
                            await _sale.UpdateSale(ViewModelUpdate);

                            AddToastSuccess(message: Resources.Messages.Successes.Thedesiredproducthasbeenaddedtothecart);
                        }

						if (User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "User")
						{
							ViewModelUpdate.Id = checksale.Id;
							ViewModelUpdate.Number = checksale.Number + Number;
                            if (checksale.Number + Number >= ViewModel.Min_Major)
                            {
                                ViewModelUpdate.Price = ViewModel.Discount_Major;
                            }
                            if (checksale.Number + Number < ViewModel.Min_Major)
                            {
                                ViewModelUpdate.Price = ViewModel.Discount_Single;
                            }
                            await _sale.UpdateSale(ViewModelUpdate);

							AddToastSuccess(message: Resources.Messages.Successes.Thedesiredproducthasbeenaddedtothecart);
						}
					}

                    if (checksale == null)
                    {
                        if (User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "Special")
                        {
                            ViewModelSale.Color = Color;
                            ViewModelSale.Number = Number;
                            ViewModelSale.ProductId = ViewModel.Id;
                            ViewModelSale.UserId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                            ViewModelSale.Price = ViewModel.Discount_Major;

                            await _sale.AddSale(ViewModelSale);

                            AddToastSuccess(message: Resources.Messages.Successes.Thedesiredproducthasbeenaddedtothecart);
                        }

                        if (User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "User")
                        {
                            ViewModelSale.Color = Color;
                            ViewModelSale.Number = Number;
                            ViewModelSale.ProductId = ViewModel.Id;
                            ViewModelSale.UserId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                            if (Number < ViewModel.Min_Major)
                            {
                                ViewModelSale.Price = ViewModel.Discount_Single;
                            }
                            if (Number >= ViewModel.Min_Major)
                            {
                                ViewModelSale.Price = ViewModel.Discount_Major;
                            }

                            await _sale.AddSale(ViewModelSale);

                            AddToastSuccess(message: Resources.Messages.Successes.Thedesiredproducthasbeenaddedtothecart);
                        }
                    }
                }
            }
        }
        return RedirectToPage("Index");
    }
}
