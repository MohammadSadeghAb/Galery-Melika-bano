using Application.TotalSaleApp;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.TotalSales;

namespace Server.Pages.Manager;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Manager)]

public class IndexModel : BasePageModel
{
    public readonly DatabaseContext _context;

    public IndexModel(DatabaseContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
    }
}
