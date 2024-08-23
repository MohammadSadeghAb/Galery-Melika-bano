using Application.ProductApp;
using Domain.SaleAgg;
using Domain.TotalSaleAgg;
using Domain.TransportCostAgg;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Persistence;
using RestSharp;
using SmsPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using zarinpalasp.netcorerest.Models;

namespace Server.Pages;

[Authorize]

public class CheckOrderModel : BasePageModel
{
    private readonly DatabaseContext _context;

    private readonly HttpClient _httpClient;

    private readonly ISaleRepository _sale;

    private readonly IProductApplication _product;

    private readonly ITotalSaleRepository _totalSale;

    private readonly ITransportCostRepository _transportCost;

    public CheckOrderModel(DatabaseContext context, ISaleRepository sale, IProductApplication product, ITotalSaleRepository totalSale, ITransportCostRepository transportCost)
    {
        _sale = sale;
        _product = product;
        _context = context;
        _totalSale = totalSale;
        _transportCost = transportCost;
        ViewModel = new();
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    string merchant = "33cdb462-cda7-4457-af7d-1629eb13f77e";
    //string amount;
    string authority;

    public string Status { get; set; }

    public string data { get; set; }

    public TotalSale ViewModel { get; set; }

    public string code { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        int weight = 0;
        int price = 0;

        Guid UserId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

        if (UserId == null)
        {
            AddToastError(message: Resources.Messages.Errors.IdIsNull);
            return Redirect("/Index");
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId);

        if (user == null)
        {
            AddToastError(message: Resources.Messages.Errors.IdIsNull);
            return Redirect("/Index");
        }

        var sales = await _context.Sales
            .Where(x => x.UserId == UserId)
            .AsNoTracking()
            .ToListAsync();

        foreach (var item in sales)
        {
            //var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);
            //product.Weight = product.Weight * item.Number;
            var product = (await _product.GetProduct(item.ProductId)).Data;
            var weights = product.Weight;
            weights = weights * item.Number;
            weight = weight + weights;
            int sum = item.Price.Value * item.Number;
            price = price + sum;
        }

        var transportcost = (await _transportCost.GetByWeight(weight));

        price = price + transportcost.Price;

        price = price * 10;

        try
        {
            VerifyParameters parameters = new VerifyParameters();


            if (HttpContext.Request.Query["Authority"] != "")
            {
                authority = HttpContext.Request.Query["Authority"];
                Status = HttpContext.Request.Query["Status"];
            }

            parameters.authority = authority;
            parameters.amount = $"{price}";
            parameters.merchant_id = merchant;


            var client = new RestClient(URLs.verifyUrl);
            RestSharp.Method method = RestSharp.Method.Post;
            var request = new RestRequest("", method);

            request.AddHeader("accept", "application/json");

            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(parameters);

            var response = client.ExecuteAsync(request);


            JObject jodata = JObject.Parse(response.Result.Content);

            data = jodata["data"].ToString();

            JObject jo = JObject.Parse(response.Result.Content);

            string errors = jo["errors"].ToString();

            if (data != "[]")
            {
                string refid = jodata["data"]["ref_id"].ToString();

                code = refid;

                //return View();
            }
            else if (errors != "[]")
            {

                string errorscode = jo["errors"]["code"].ToString();

                //return BadRequest($"error code {errorscode}");

            }


        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

        if (Status == "OK" && data != "[]")
        {

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

            ViewModel = await _context.TotalSales.FirstOrDefaultAsync(x => x.FactorNumber == max + 1);

            try
            {

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
                            //return RedirectToPage("/CheckOrder");
                        }
                    }

                    AddToastSuccess(Resources.Messages.Successes.Yourorderhasbeenregistered);
                }
            }
            catch (Exception)
            {

                
            }
        }
        return Page();
    }
}