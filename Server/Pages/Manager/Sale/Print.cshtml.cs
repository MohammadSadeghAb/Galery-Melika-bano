using Application.ProductApp;
using Application.TotalSaleApp;
using Application.UserApp;
using Domain.ProductAgg;
using Domain.TotalSaleAgg;
using Domain.Users;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Users;

namespace Server.Pages.Manager.Sale;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Constants.Role.Manager)]

public class PrintModel : BasePageModel
{
    private readonly ITotalSaleApplication _totalSaleApplication;

    private readonly IUserApplication _userApplication;

    public readonly DatabaseContext _context;

    public PrintModel(ITotalSaleApplication totalSaleApplication,
                      IUserApplication userApplication,
                      DatabaseContext context)
    {
        _context = context;
        _userApplication = userApplication;
        _totalSaleApplication = totalSaleApplication;
        ViewModelUser = new();
        ViewModelTotalSale = new ();
        TotalSales = new List<TotalSale>();
    }

    public ViewModels.Pages.Admin.TotalSales.DetailsViewModel ViewModelTotalSale { get; set; }

    public IList<TotalSale> TotalSales { get; set; }

    public DetailsViewModel ViewModelUser { get; set; }

    public DetailsViewModel AdminUser { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError
                (message: Resources.Messages.Errors.IdIsNull);
            return RedirectToPage(pageName: "Index");
        }

        ViewModelTotalSale = (await _totalSaleApplication.GetTotalSale(id.Value)).Data;

        TotalSales = await _context.TotalSales.Where(x => x.FactorNumber == ViewModelTotalSale.FactorNumber).ToListAsync();

        ViewModelUser = (await _userApplication.GetUser(ViewModelTotalSale.UserId)).Data;

        Guid iduser = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
        AdminUser = (await _userApplication.GetUser(iduser)).Data;

        if (ViewModelTotalSale == null)
        {
            AddToastError
                (message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage(pageName: "Index");
        }

        if (ViewModelUser == null)
        {
            AddToastError
                (message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage(pageName: "Index");
        }

        return Page();
    }
}
