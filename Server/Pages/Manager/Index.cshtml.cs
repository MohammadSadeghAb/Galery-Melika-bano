using Application.TotalSaleApp;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Resources;
using SmsPanel.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.TotalSales;
using static System.Net.Mime.MediaTypeNames;

namespace Server.Pages.Manager;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Manager)]

public class IndexModel : BasePageModel
{
    private readonly ITotalSaleApplication _application;

    public readonly DatabaseContext _context;

    private readonly HttpClient _httpClient;

    public IndexModel(ITotalSaleApplication application, DatabaseContext context)
    {
        _application = application;
        _context = context;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }

    [BindProperty]
    public int From { get; set; }
    [BindProperty]
    public int Until { get; set; }

    public async Task OnGetAsync()
    {
    }

    public async Task<IActionResult> OnPostAsync(string status)
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        if (status == "accepted")
        {
            var list = await (_context.TotalSales.Where(x => x.FactorNumber >= From).Where(x => x.FactorNumber <= Until).ToListAsync());

            foreach (var i in list)
            {
                var sale = (await _application.GetTotalSale(i.Id)).Data;

                var saleacceptrd = await _context.TotalSales.Where(x => x.FactorNumber == sale.FactorNumber).ToListAsync();

                foreach (var item in saleacceptrd)
                {
                    item.Accepted = true;

                    _context.TotalSales.Update(item);
                }

            }
            await _context.SaveChangesAsync();

            var successMessage = string.Format(Resources.Messages.Successes.Updated, Resources.DataDictionary.Sales);

            AddToastSuccess(successMessage);

            return Page();
        }
        else if(status == "packing")
        {
            var list = await (_context.TotalSales.Where(x => x.FactorNumber >= From).Where(x => x.FactorNumber <= Until).ToListAsync());

            foreach (var i in list)
            {
                var sale = (await _application.GetTotalSale(i.Id)).Data;

                var saleacceptrd = await _context.TotalSales.Where(x => x.FactorNumber == sale.FactorNumber).ToListAsync();

                foreach (var item in saleacceptrd)
                {
                    item.Packing = true;

                    _context.TotalSales.Update(item);
                }

            }
            await _context.SaveChangesAsync();

            var successMessage = string.Format(Resources.Messages.Successes.Updated, Resources.DataDictionary.Sales);

            AddToastSuccess(successMessage);

            return Page();
        }
        else if (status == "posted")
        {
            var list = await (_context.TotalSales.Where(x => x.FactorNumber >= From).Where(x => x.FactorNumber <= Until).ToListAsync());

            foreach (var i in list)
            {
                var sale = (await _application.GetTotalSale(i.Id)).Data;

                var saleacceptrd = await _context.TotalSales.Where(x => x.FactorNumber == sale.FactorNumber).ToListAsync();

                foreach (var item in saleacceptrd)
                {
                    item.Posted = true;

                    _context.TotalSales.Update(item);
                }

                var totalsale = (await _application.GetTotalSale(i.Id)).Data;

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == totalsale.UserId);

                var stringContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("Token","7TEEwvtO5H4AYUgnPttu6X9m6i0ix02V"),
                new KeyValuePair<string, string>("To",$"{user.CellPhoneNumber}"),
                new KeyValuePair<string, string>("Message","فروشگاه ملیکا بانو 🌸🦢 " + "\n" + $"سلام دوست عزیزم، سفارش {totalsale.FactorNumber} ارسال شد." + "\n" + "شما میتونید کد رهگیری پستی خود را از طریق صفحه زیر پیگیری کنید" + "\n" + "https://eitaa.com/joinchat/3039035697C8dcf2f5165" + "\n" + " و در پایان، ممنون که از تولیدات ایرانی وحجاب حمایت میکنید🌷🙏😊" + "\n" + "لغو 11"),
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
                    //if (sendOutputModel.Status == 100)
                    //{
                    //    AddToastSuccess("پیامک باموفقیت ارسال شد");
                    //    return RedirectToPage("TotalSale_Packing");
                    //}
                    //else
                    //{
                    //    AddToastError("پیامک باموفقیت ارسال نشد");
                    //    return RedirectToPage("TotalSale_Packing");
                    //}
                }

            }
            await _context.SaveChangesAsync();

            var successMessage = string.Format(Resources.Messages.Successes.Updated, Resources.DataDictionary.Sales);

            AddToastSuccess(successMessage);

            return Page();
        }
        else if (status == "delivery")
        {
            var list = await (_context.TotalSales.Where(x => x.FactorNumber >= From).Where(x => x.FactorNumber <= Until).ToListAsync());

            foreach (var i in list)
            {
                var sale = (await _application.GetTotalSale(i.Id)).Data;

                var saleacceptrd = await _context.TotalSales.Where(x => x.FactorNumber == sale.FactorNumber).ToListAsync();

                foreach (var item in saleacceptrd)
                {
                    item.Delivery = true;

                    _context.TotalSales.Update(item);
                }

            }
            await _context.SaveChangesAsync();

            var successMessage = string.Format(Resources.Messages.Successes.Updated, Resources.DataDictionary.Sales);

            AddToastSuccess(successMessage);

            return Page();
        }

        return Page();
    }
}
