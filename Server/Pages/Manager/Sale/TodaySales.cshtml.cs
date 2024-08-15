using Domain.TotalSaleAgg;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Manager.Sale;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Constants.Role.Manager)]

public class TodaySalesModel : BasePageModel
{
    public readonly DatabaseContext _context;

    public TodaySalesModel(DatabaseContext context)
    {
        _context = context;
        ViewModel = new List<TotalSale>();
    }

    public IList<TotalSale> ViewModel { get; set; }

    [BindProperty]
    public int factor { get; set; }

    public async Task OnGetAsync()
    {
        DateTime dt = Framework.Utility.Now.Date;

        ViewModel = await _context.TotalSales.Where(x => x.InsertDateTime.Date == dt).OrderBy(x => x.FactorNumber).ToListAsync();
        ViewModel = ViewModel.DistinctBy(x => x.FactorNumber).ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (factor == null)
        {
            AddToastError(message: Resources.Messages.Errors.Pleaseenterthefactornumber);

            ViewModel = await _context.TotalSales.OrderBy(x => x.FactorNumber).ToListAsync();

            return Page();
        }

        ViewModel = await _context.TotalSales.Where(x => x.FactorNumber == factor).ToListAsync();

        return Page();
    }
}
