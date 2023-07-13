using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.User;

[Authorize]

public class IndexModel : BasePageModel
{
    public readonly DatabaseContext _context;

    public IndexModel(DatabaseContext context)
    {
        _context = context;
    }

    public int CountCard { get; set; }

    public int CountTotal { get; set; }

    public int CountDelivery { get; set; }

    public async Task OnGetAsync(Guid? id)
    {
        CountCard = _context.Sales.Where(x => x.UserId == id.Value).Count();

        CountTotal = _context.TotalSales.Where(x => x.UserId == id.Value).Count();

        CountDelivery = _context.TotalSales.Where(x => x.UserId == id.Value && x.Delivery == true).Count();
    }
}
