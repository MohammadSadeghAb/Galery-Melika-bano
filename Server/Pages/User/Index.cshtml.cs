using Application.ProductApp;
using Application.SaleApp;
using Domain.SaleAgg;
using Domain.TotalSaleAgg;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Resources;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.User;

[Authorize]
public class IndexModel : BasePageModel
{
    private readonly ITotalSaleRepository _totalSale;

    private readonly ISaleApplication _application;

    private readonly IProductApplication _product;

    private readonly ISaleRepository _sale;

    public readonly DatabaseContext _context;

    public IndexModel(ITotalSaleRepository totalSale,
                      DatabaseContext context,
                      ISaleRepository sale,
                      ISaleApplication application,
                      IProductApplication product)
    {
        _sale = sale;
        _product = product;
        _context = context;
        _totalSale = totalSale;
        _application = application;
        ViewModel = new();
    }

    public Guid UserId { get; set; }

    public TotalSale ViewModel { get; set; }

    public int? pricetotal { get; set; } = 0;

    public int? numbertotals { get; set; } = 0;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError
                (message: Resources.Messages.Errors.IdIsNull);

            return RedirectToPage(pageName: "Index");
        }

        UserId = id.Value;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        await _application.DeleteSale(id.Value);

        UserId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

        AddToastSuccess(Resources.Messages.Successes.Theitemhasbeensuccessfullyremovedfromyourshoppingcart);

        return Page();
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        UserId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId);

        if (user.Address == null || user.ProvinceAddress == null || user.CityAddress == null ||
            user.NationalCode == null || user.PostalCode == null)
        {
            return RedirectToPage("/Account/User/Update");
        }

        var sales = await _context.Sales
            .Where(x => x.UserId == UserId)
            .AsNoTracking()
            .ToListAsync();

        int max = 0;

        var check = _context.TotalSales.ToList();

        if (check.Count != 0)
        {
            max = _context.TotalSales.Max(x => x.FactorNumber);
        }

        bool a = false;

        foreach (var item in sales)
        {
            var product = (await _product.GetProduct(item.ProductId)).Data;

            if (item.Number <= product.Number)
            {
                product.Number = product.Number - item.Number;
                var _saleModel = new TotalSale
                {
                    Color = item.Color,
                    Number = item.Number,
                    Products = item.ProductId,
                    UserId = item.UserId,
                    TotalPrice = item.Price * item.Number,
                    FactorNumber = max + 1,
                };

                _sale.Remove(item);

                await _totalSale.CreateAsync(_saleModel);

                await _product.UpdateProduct(product);

                a = true;
            }
        }

        await _sale.SaveChangesAsync();
        await _totalSale.SaveChangesAsync();

        if (a == true)
        {
            AddToastSuccess(Resources.Messages.Successes.Yourorderhasbeenregistered);
        }

        return RedirectToPage("/Index");
    }
}
