using Domain.TotalSaleAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages;

[Authorize]

public class CheckOrderModel : PageModel
{
    private readonly DatabaseContext _context;

    public CheckOrderModel(DatabaseContext context)
    {
        _context = context;
        ViewModel = new();
    }

    public TotalSale ViewModel { get; set; }

    public async Task OnGetAsync()
    {
        Guid userid = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
        int a = await _context.TotalSales.Where(x => x.UserId == userid).MaxAsync(x => x.FactorNumber);
        ViewModel = await _context.TotalSales.FirstOrDefaultAsync(x => x.FactorNumber == a);
    }
}
