using Application.ProductApp;
using Application.SaleApp;
using Application.UserApp;
using Domain.CategoryAgg;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Resources;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Sales;
using ViewModels.Shared;

namespace Server.Pages.Admin.Sales;

[Authorize(Roles = Constants.Role.Admin)]

public class CreateModel : BasePageModel
{
    private readonly ISaleApplication _application;

    private readonly DatabaseContext _context;

    public CreateModel(ISaleApplication application,
                       DatabaseContext context)
    {
        _context = context;
        _application = application;
        users = new();
        products = new();
        ViewModel = new();
    }

    [BindProperty]
    public CreateViewModel ViewModel { get; set; }

    public List<KeyValueViewModel> users { get; set; }

    public List<KeyValueViewModel> products { get; set; }

    public async Task OnGetAsync()
    {
        var Users = await _context.Users.ToListAsync();
        foreach (var item in Users)
        {
            users.Add(new KeyValueViewModel()
            {
                Id = item.Id,
                Name = item.FullName,
            });
        }

        var Products = await _context.Products.ToListAsync();
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
            return Page();
        }

        var res = await _application.AddSale(ViewModel);

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
