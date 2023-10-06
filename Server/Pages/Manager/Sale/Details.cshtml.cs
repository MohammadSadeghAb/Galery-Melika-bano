using Application.ProductApp;
using Application.TotalSaleApp;
using Application.UserApp;
using Domain.TotalSaleAgg;
using Framework.OperationResult;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.TotalSales;
using static System.Net.Mime.MediaTypeNames;

namespace Server.Pages.Manager.Sale;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Manager)]

public class DetailsModel : BasePageModel
{
    private readonly ITotalSaleApplication _totalsale;

    private readonly IUserApplication _user;

    public readonly DatabaseContext _context;

    public DetailsModel(IUserApplication user,
                        ITotalSaleApplication totalsale,
                        DatabaseContext context)
    {
        _user = user;
        _context = context;
        _totalsale = totalsale;
        ViewModelTotalSale = new();
        ViewModel = new List<TotalSale>();
    }

    public DetailsViewModel ViewModelTotalSale { get; set; }

    public IList<TotalSale> ViewModel { get; set; }

    public OperationResultWithData<ViewModels.Pages.Admin.Users.DetailsViewModel> User { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError
                (message: Resources.Messages.Errors.IdIsNull);

            return RedirectToPage(pageName: "Index");
        }

        ViewModelTotalSale = (await _totalsale.GetTotalSale(id.Value)).Data;

        ViewModel = await _context.TotalSales.Where(x => x.FactorNumber == ViewModelTotalSale.FactorNumber).ToListAsync();

        if (ViewModelTotalSale == null)
        {
            AddToastError
                (message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage(pageName: "Index");
        }

        User = await _user.GetUser(ViewModelTotalSale.UserId);

        return Page();
    }
}
