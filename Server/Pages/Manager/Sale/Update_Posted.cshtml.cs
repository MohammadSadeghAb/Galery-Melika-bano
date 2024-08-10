using Application.TotalSaleApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence;
using Resources;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using SmsPanel.Models;
using System.Net.Http.Headers;
using System.Collections.Generic;
using ViewModels.Pages.Admin.TotalSales;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Server.Pages.Manager.Sale;

[Authorize(Roles = Infrastructure.Constants.Role.Manager)]

public class Update_PostedModel : BasePageModel
{
    private readonly ITotalSaleApplication _application;

    private readonly DatabaseContext _context;

    private readonly HttpClient _httpClient;

    public Update_PostedModel(ITotalSaleApplication application, DatabaseContext context)
    {
        _context = context;
        _application = application;
        ViewModel = new();
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }

    [BindProperty]
    public UpdateViewModel ViewModel { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError(Resources.Messages.Errors.IdIsNull);

            return RedirectToPage("TotalSale_Packing");
        }

        ViewModel = (await _application.GetTotalSale(id.Value)).Data;

        if (ViewModel == null)
        {
            AddToastError(Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage("TotalSale_Packing");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        var sale = (await _application.GetTotalSale(ViewModel.Id.Value)).Data;

        var salepost = await _context.TotalSales.Where(x => x.FactorNumber == sale.FactorNumber).ToListAsync();

        foreach (var item in salepost)
        {
            item.Accepted = true;
            item.Packing = true;
            item.Posted = ViewModel.Posted;

            _context.TotalSales.Update(item);
        }

        await _context.SaveChangesAsync();

        var successMessage = string.Format(Resources.Messages.Successes.Updated, DataDictionary.Sale);

        AddToastSuccess(successMessage);

        if (ViewModel.Posted == true)
        {
            var totalsale = (await _application.GetTotalSale(ViewModel.Id.Value)).Data;

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == totalsale.UserId);

            var stringContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Token","7TEEwvtO5H4AYUgnPttu6X9m6i0ix02V"),
                new KeyValuePair<string, string>("To",$"{user.CellPhoneNumber}"),
                new KeyValuePair<string, string>("Message",$"فروشگاه ملیکا بانو سلام دوست عزیزم سفارش شما با شماره فاکتور : {totalsale.FactorNumber} ارسال شد " + "شما میتونید کد رهگیری پستی خودتون رو از طریق این صفحه بگیرید" + "https://eitaa.com/joinchat/3039035697C8dcf2f5165" + "و مرسوله خودتونو پیگیری کنید و در پایان ممنون که از تولیدات ایرانی حمایت میکنید"),
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
                    AddToastSuccess("پیامک باموفقیت ارسال شد");
                    return RedirectToPage("TotalSale_Packing");
                }
                else
                {
                    AddToastError("پیامک باموفقیت ارسال نشد");
                    return RedirectToPage("TotalSale_Packing");
                }
            }
        }

        return RedirectToPage("TotalSale_Packing");
    }
}
