using Application.TotalSaleApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence;
using Resources;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using ViewModels.Pages.Admin.TotalSales;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Manager.Sale;

[Authorize(Roles = Infrastructure.Constants.Role.Manager)]

public class Update_PackingModel : BasePageModel
{
    private readonly ITotalSaleApplication _application;

    private readonly DatabaseContext _context;

    public Update_PackingModel(ITotalSaleApplication application, DatabaseContext context)
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

            return RedirectToPage("TotalSale_Accept");
        }

        ViewModel = (await _application.GetTotalSale(id.Value)).Data;

        if (ViewModel == null)
        {
            AddToastError(Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage("TotalSale_Accept");
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

        var salepacking = await _context.TotalSales.Where(x => x.FactorNumber == sale.FactorNumber).ToListAsync();

        foreach (var item in salepacking)
        {
            item.Packing = ViewModel.Packing;

            _context.TotalSales.Update(item);
        }

        await _context.SaveChangesAsync();

        var successMessage = string.Format(Resources.Messages.Successes.Updated, DataDictionary.Sale);

        AddToastSuccess(successMessage);

        return RedirectToPage("TotalSale_Accept");
    }
}
