using Application.TotalSaleApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Resources;
using SmsPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.TotalSales;

namespace Server.Pages.Manager.Sale;

[Authorize(Roles = Infrastructure.Constants.Role.Manager)]

public class UpdateModel : BasePageModel
{
    private readonly ITotalSaleApplication _application;

    private readonly DatabaseContext _context;

    public UpdateModel(ITotalSaleApplication application, DatabaseContext context)
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

            return RedirectToPage("TotalSale");
        }

        ViewModel = (await _application.GetTotalSale(id.Value)).Data;

        if (ViewModel == null)
        {
            AddToastError(Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage("TotalSale");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        var res = await _application.UpdateTotalSale(ViewModel);

        if (res.Succeeded == false || res.ErrorMessages.Count > 0)
        {
            foreach (var item in res.ErrorMessages)
            {
                AddToastError(item);
            }

            return Page();
        }

        var successMessage = string.Format(Resources.Messages.Successes.Updated, DataDictionary.Sale);

        AddToastSuccess(successMessage);

        return RedirectToPage("TotalSale");
    }
}
