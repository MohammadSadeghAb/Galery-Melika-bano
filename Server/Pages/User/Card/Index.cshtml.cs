using Application.ProductApp;
using Application.SaleApp;
using Domain.SaleAgg;
using Domain.TotalSaleAgg;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parbad;
using Parbad.Gateway.ZarinPal;
using Persistence;
using Resources;
using SmsPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Sales;

namespace Server.Pages.User.Card;

[Authorize]
public class IndexModel : BasePageModel
{
    private readonly ITotalSaleRepository _totalSale;

    private readonly ISaleApplication _application;

    private readonly IProductApplication _product;

    private readonly ISaleRepository _sale;

    public readonly DatabaseContext _context;

    private readonly HttpClient _httpClient;

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
        ViewModelSale = new();
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public Guid UserId { get; set; }

    public TotalSale ViewModel { get; set; }

    [BindProperty]
    public UpdateViewMode ViewModelSale { get; set; }

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

    public async Task<IActionResult> OnPostAsync(Guid? id, string check)
    {
        UserId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

        if (id.HasValue == false)
        {
            AddToastError(Resources.Messages.Errors.IdIsNull);
            return Page();
        }

        ViewModelSale.Id = id.Value;

        if (ViewModelSale.Number == 1 && check == "low")
        {
            await _application.DeleteSale(ViewModelSale.Id.Value);
        }

        if (check == "add")
        {
            ViewModelSale.Number = ViewModelSale.Number + 1;
            await _application.UpdateSale(ViewModelSale);
        }

        if (check == "low")
        {
            ViewModelSale.Number = ViewModelSale.Number - 1;
            await _application.UpdateSale(ViewModelSale);
        }

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

        Random random = new Random();
        int tracking = random.Next(100000, 999999);

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
                    TotalPrice = item.Price,
                    FactorNumber = max + 1,
                    TrackingCode = tracking.ToString(),
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
            int factor = max + 1;

            var stringContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Token","7TEEwvtO5H4AYUgnPttu6X9m6i0ix02V"),
                new KeyValuePair<string, string>("To",$"{user.CellPhoneNumber}"),
                new KeyValuePair<string, string>("Message",$"{Resources.Messages.Successes.Youritemhasbeenregistered} {Resources.DataDictionary.FactorNumber} : {factor}"),
                new KeyValuePair<string, string>("Sender","238")
            });

            var postTask = _httpClient.PostAsync("http://panelyab.com/api/send", stringContent);
            postTask.Wait();

            var response = postTask.Result;
            if (response.IsSuccessStatusCode)
            {
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var readTask = response.Content.ReadAsAsync<SendOutputModel>();
                readTask.Wait();

                var sendOutputModel = readTask.Result;
                if (sendOutputModel.Status == 100)
                {
                    AddToastSuccess(Resources.Messages.Successes.Yourorderhasbeenregistered);
                    //return RedirectToPage("/Index");
                    return RedirectToPage("/CheckOrder");
                }
            }

            AddToastSuccess(Resources.Messages.Successes.Yourorderhasbeenregistered);
        }

        return RedirectToPage("/CheckOrder");
    }
}
