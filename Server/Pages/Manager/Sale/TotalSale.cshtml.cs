using Application.TotalSaleApp;
using Domain.TotalSaleAgg;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.TotalSales;

namespace Server.Pages.Manager.Sale;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Constants.Role.Manager)]

public class TotalSaleModel : BasePageModel
{
    private readonly ITotalSaleApplication _application;

    public readonly DatabaseContext _context;

    public TotalSaleModel(ITotalSaleApplication application,
                      DatabaseContext context)
    {
        _context = context;
        _application = application;
        ViewModel = new List<TotalSale>();
    }

    public IList<TotalSale> ViewModel { get; set; }

    [BindProperty]
    public int factor { get; set; } = 0;

    public async Task OnGetAsync()
    {
        ViewModel = await _context.TotalSales.OrderBy(x => x.FactorNumber).ToListAsync();
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
