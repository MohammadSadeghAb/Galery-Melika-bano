using Application.ProductApp;
using Application.TotalSaleApp;
using Application.UserApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.TotalSales;
using ViewModels.Shared;

namespace Server.Pages.Admin.TotalSales;

[Authorize(Roles = Constants.Role.Admin)]

public class CreateModel : BasePageModel
{
    private readonly ITotalSaleApplication _application;

    private readonly IUserApplication _user;

    private readonly IProductApplication _product;

    public CreateModel(ITotalSaleApplication application,
                       IUserApplication user,
                       IProductApplication product)
    {
        _user = user;
        _product = product;
        _application = application;
        users = new();
        products = new();
        ViewModel = new();
    }

    [BindProperty]
    public CommonViewModel ViewModel { get; set; }

    public ViewModels.Pages.Admin.Products.UpdateViewModel UpdateProduct { get; set; }

    public List<KeyValueViewModel> users { get; set; }

    public List<KeyValueViewModel> products { get; set; }

    public async Task OnGetAsync()
    {
        var Users = (await _user.GetAllUsers()).Data;
        foreach (var item in Users)
        {
            users.Add(new KeyValueViewModel()
            {
                Id = item.Id,
                Name = item.FullName,
            });
        }

        var Products = (await _product.GetAllProduct()).Data;
        foreach (var item in Products)
        {
            products.Add(new KeyValueViewModel()
            {
                Id = item.Id,
                Name = item.Name_Product,
            });
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid == false)
        {
            var Users = (await _user.GetAllUsers()).Data;
            foreach (var item in Users)
            {
                users.Add(new KeyValueViewModel()
                {
                    Id = item.Id,
                    Name = item.FullName,
                });
            }

            var Products = (await _product.GetAllProduct()).Data;
            foreach (var item in Products)
            {
                products.Add(new KeyValueViewModel()
                {
                    Id = item.Id,
                    Name = item.Name_Product,
                });
            }

            return Page();
        }

        UpdateProduct = (await _product.GetProduct(ViewModel.Products)).Data;

        if (UpdateProduct.Number < ViewModel.Number)
        {
            var Users = (await _user.GetAllUsers()).Data;
            foreach (var item in Users)
            {
                users.Add(new KeyValueViewModel()
                {
                    Id = item.Id,
                    Name = item.FullName,
                });
            }

            var Products = (await _product.GetAllProduct()).Data;
            foreach (var item in Products)
            {
                products.Add(new KeyValueViewModel()
                {
                    Id = item.Id,
                    Name = item.Name_Product,
                });
            }

            AddToastError(message: Resources.Messages.Errors.Thisnumberofproductsisnotavailable);
            return Page();
        }

        UpdateProduct.Number = UpdateProduct.Number - ViewModel.Number.Value;

        await _product.UpdateProduct(UpdateProduct);

        //Random random = new Random();
        //ViewModel.TrackingCode = random.Next(100000, 999999).ToString();

        var res = await _application.AddTotalSale(ViewModel);

        if (res.Succeeded == false || res.ErrorMessages.Count > 0)
        {
            foreach (var item in res.ErrorMessages)
            {
                AddToastError(item);
            }

            return Page();
        }

        var successMessage = string.Format(Resources.Messages.Successes.Created, DataDictionary.Sale);

        AddToastSuccess(successMessage);

        return RedirectToPage("Index");
    }
}
