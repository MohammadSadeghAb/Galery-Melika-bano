using Application.TotalSaleApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence;
using Resources;
using SmsPanel.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Linq;
using ViewModels.Pages.Admin.TotalSales;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Manager.Sale;

[Authorize(Roles = Infrastructure.Constants.Role.Manager)]

public class Update_DeliveryModel : BasePageModel
{
    private readonly ITotalSaleApplication _application;

    private readonly DatabaseContext _context;

    public Update_DeliveryModel(ITotalSaleApplication application, DatabaseContext context)
    {
        _context = context;
        _application = application;
        ViewModel = new();
    }

    [BindProperty]
    public UpdateViewModel ViewModel { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError(Resources.Messages.Errors.IdIsNull);

            return RedirectToPage("TotalSalePost");
        }

        ViewModel = (await _application.GetTotalSale(id.Value)).Data;

        if (ViewModel == null)
        {
            AddToastError(Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage("TotalSalePost");
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
            item.Posted = true;
            item.Delivery = ViewModel.Delivery;

            _context.TotalSales.Update(item);
        }

        await _context.SaveChangesAsync();

        var successMessage = string.Format(Resources.Messages.Successes.Updated, DataDictionary.Sale);

        AddToastSuccess(successMessage);

        return RedirectToPage("TotalSalePost");
    }
}
