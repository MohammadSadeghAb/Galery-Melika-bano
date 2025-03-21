﻿using Application.ProductApp;
using Application.SaleApp;
using Application.TransportCostApp;
using Domain.CategoryAgg;
using Domain.ProductAgg;
using Domain.SaleAgg;
using Domain.TotalSaleAgg;
using Domain.Users;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Persistence;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Sales;
using zarinpalasp.netcorerest.Models;

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

    private readonly IWebHostEnvironment _webHostEnvironment;

    public readonly ITransportCostApplication _transportCost;

    public IndexModel(ITotalSaleRepository totalSale,
                      DatabaseContext context,
                      ISaleRepository sale,
                      ISaleApplication application,
                      IProductApplication product,
                      ITransportCostApplication transportCost,
                      IWebHostEnvironment webHostEnvironment)
    {
        _sale = sale;
        _product = product;
        _context = context;
        _totalSale = totalSale;
        _application = application;
        _transportCost = transportCost;
        _webHostEnvironment = webHostEnvironment;
        ViewModel = new();
        ViewModelSale = new();
        Sales = new();
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    }

    public string merchant { get; set; }

    public string amount { get; set; }

    public string authority { get; set; }

    public string description { get; set; }

    public string callbackurl { get; set; }

    public Guid UserId { get; set; }

    public TotalSale ViewModel { get; set; }

    public List<Sale> Sales { get; set; }

    [BindProperty]
    public UpdateViewMode ViewModelSale { get; set; }

    public Product ViewModelProduct { get; set; }

    public int? pricetotal { get; set; } = 0;

    public int weighttotal { get; set; } = 0;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError
                (message: Resources.Messages.Errors.IdIsNull);

            return RedirectToPage(pageName: "Index");
        }

        UserId = id.Value;

        Sales = await _context.Sales.Where(x => x.UserId == UserId).ToListAsync();

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
        var sale = await _context.Sales.FirstOrDefaultAsync(x => x.Id == ViewModelSale.Id);
        ViewModelProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == sale.ProductId);

        if (ViewModelSale.Number == 1 && check == "low")
        {
            await _application.DeleteSale(ViewModelSale.Id.Value);
        }

        if (check == "add")
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == ViewModelProduct.CategoryParent_Id);

            if (category.Name == Resources.DataDictionary.Free)
            {
                var checksalefree = await _context.Sales.FirstOrDefaultAsync(x => x.UserId == UserId && x.ProductId == ViewModelProduct.Id);
                var checktotalsalefree = await _context.Sales.FirstOrDefaultAsync(x => x.UserId == UserId && x.ProductId == ViewModelProduct.Id);

                if (checksalefree != null && checksalefree.Number >= 5 && checktotalsalefree != null && checktotalsalefree.Number >= 5)
                {
                    AddToastError(message: Resources.Messages.Errors.Thisnumberofproductsisnotavailable);
                    return Page();
                }
            }

            ViewModelSale.Number = ViewModelSale.Number + 1;
            if (ViewModelSale.Number >= ViewModelProduct.Min_Major)
            {
                ViewModelSale.Price = ViewModelProduct.Discount_Major;
            }
            if (ViewModelSale.Number < ViewModelProduct.Min_Major)
            {
                ViewModelSale.Price = ViewModelProduct.Discount_Single;
            }
            await _application.UpdateSale(ViewModelSale);
        }

        if (check == "low")
        {
            ViewModelSale.Number = ViewModelSale.Number - 1;
            if (ViewModelSale.Number >= ViewModelProduct.Min_Major)
            {
                ViewModelSale.Price = ViewModelProduct.Discount_Major;
            }
            if (ViewModelSale.Number < ViewModelProduct.Min_Major)
            {
                ViewModelSale.Price = ViewModelProduct.Discount_Single;
            }
            await _application.UpdateSale(ViewModelSale);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        int price = 0;

        int weight = 0;

        UserId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId);

        if (user.Address == null || user.ProvinceAddress == null || user.CityAddress == null ||
            user.CellPhoneNumber == null)
        {
            return RedirectToPage("/Account/User/Update");
        }

        var sales = await _context.Sales
            .Where(x => x.UserId == UserId)
            .AsNoTracking()
            .ToListAsync();

        foreach (var item in sales)
        {
            //var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);
            var product = (await _product.GetProduct(item.ProductId)).Data;
            if (item.Number <= product.Number)
            {
                var weights = product.Weight;
                weights = weights * item.Number;
                weight = weight + weights;
                int sum = item.Price.Value * item.Number;
                price = price + sum;
            }
        }

        var transportcost = (await _transportCost.GetByWeight(weight)).Data;

        price = price + transportcost.Price;

        price = price * 10;

        //int max = 0;

        //Random random = new Random();
        //int tracking = random.Next(100000, 999999);

        //var check = _context.TotalSales.ToList();

        //if (check.Count != 0)
        //{
        //    max = _context.TotalSales.Max(x => x.FactorNumber);
        //}

        //bool a = false;

        //foreach (var item in sales)
        //{
        //    var product = (await _product.GetProduct(item.ProductId)).Data;

        //    if (item.Number <= product.Number)
        //    {
        //        product.Number = product.Number - item.Number;
        //        var _saleModel = new TotalSale
        //        {
        //            Color = item.Color,
        //            Number = item.Number,
        //            Products = item.ProductId,
        //            UserId = item.UserId,
        //            TotalPrice = item.Price,
        //            FactorNumber = max + 1,
        //            TrackingCode = tracking.ToString(),
        //        };

        //        _sale.Remove(item);

        //        await _totalSale.CreateAsync(_saleModel);

        //        await _product.UpdateProduct(product);

        //        a = true;
        //    }
        //}

        //await _sale.SaveChangesAsync();
        //await _totalSale.SaveChangesAsync();

        //if (a == true)
        //{
        //    int factor = max + 1;

        //    var stringContent = new FormUrlEncodedContent(new[]
        //    {
        //        new KeyValuePair<string, string>("Token","7TEEwvtO5H4AYUgnPttu6X9m6i0ix02V"),
        //        new KeyValuePair<string, string>("To",$"{user.CellPhoneNumber}"),
        //        new KeyValuePair<string, string>("Message",$"{Resources.Messages.Successes.Youritemhasbeenregistered} {Resources.DataDictionary.FactorNumber} : {factor}"),
        //        new KeyValuePair<string, string>("Sender","238")
        //    });

        //    var postTask = _httpClient.PostAsync("http://panelyab.com/api/send", stringContent);
        //    postTask.Wait();

        //    var response = postTask.Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        var readTask = response.Content.ReadAsAsync<SendOutputModel>();
        //        readTask.Wait();

        //        var sendOutputModel = readTask.Result;
        //        if (sendOutputModel.Status == 100)
        //        {
        //            AddToastSuccess(Resources.Messages.Successes.Yourorderhasbeenregistered);
        //            //return RedirectToPage("/Index");
        //            return RedirectToPage("/CheckOrder");
        //        }
        //    }

        //    AddToastSuccess(Resources.Messages.Successes.Yourorderhasbeenregistered);
        //}

        //try
        //{

        //    using (var client = new HttpClient())
        //    {
        //        amount = $"{price}";
        //        RequestParameters parameters = new RequestParameters(merchant, amount, description, callbackurl, "", "");

        //        var json = JsonConvert.SerializeObject(parameters);

        //        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response = await client.PostAsync(URLs.requestUrl, content);

        //        string responseBody = await response.Content.ReadAsStringAsync();

        //        JObject jo = JObject.Parse(responseBody);
        //        string errorscode = jo["errors"].ToString();

        //        JObject jodata = JObject.Parse(responseBody);
        //        string dataauth = jodata["data"].ToString();
        //        if (dataauth != "[]")
        //        {
        //            authority = jodata["data"]["authority"].ToString();

        //            string gatewayUrl = URLs.gateWayUrl + authority;

        //            return Redirect(gatewayUrl);

        //        }
        //        else
        //        {

        //            return BadRequest("error " + errorscode);


        //        }

        //    }


        //}

        //catch (Exception ex)
        //{
        //    throw new Exception(ex.Message);


        //}

        try
        {
            amount = $"{price}";
            merchant = "33cdb462-cda7-4457-af7d-1629eb13f77e";
            description = "خرید از ملیکا بانو";
            callbackurl = string.Concat(HttpContext.Request.Scheme, "://", HttpContext.Request.Host.Value, "/CheckOrder");

            zarinpalasp.netcorerest.Models.RequestParameters Parameters = new zarinpalasp.netcorerest.Models.RequestParameters(merchant, amount, description, callbackurl, "", "");



            //be dalil in ke metadata be sorate araye ast va do meghdare mobile va email dar metadata gharar mmigirad
            //shoma mitavanid in maghadir ra az kharidar begirid va set konid dar gheir in sorat khali ersal konid

            var client = new RestClient(URLs.requestUrl);

            RestSharp.Method method = RestSharp.Method.Post;

            var request = new RestRequest("", method);

            request.AddHeader("accept", "application/json");

            request.AddHeader("content-type", "application/json");

            request.AddJsonBody(Parameters);

            var requestresponse = client.ExecuteAsync(request);

            JObject jo = JObject.Parse(requestresponse.Result.Content);

            string errorscode = jo["errors"].ToString();

            JObject jodata = JObject.Parse(requestresponse.Result.Content);

            string dataauth = jodata["data"].ToString();


            if (dataauth != "[]")
            {


                authority = jodata["data"]["authority"].ToString();

                string gatewayUrl = URLs.gateWayUrl + authority;

                return Redirect(gatewayUrl);

            }
            else
            {


                return BadRequest("error " + errorscode);


            }


        }

        catch (Exception ex)
        {
            //    throw new Exception(ex.Message);


        }

        return RedirectToPage("/CheckOrder");
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        UserId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

        var sales = await _context.Sales.Where(x => x.UserId == UserId).ToListAsync();

        foreach (var item in sales)
        {
            _context.Sales.Remove(item);
        }

        await _context.SaveChangesAsync();

        return Page();
    }
}
