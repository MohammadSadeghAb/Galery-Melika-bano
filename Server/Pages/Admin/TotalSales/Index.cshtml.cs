using Application.TotalSaleApp;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.TotalSales;

namespace Server.Pages.Admin.TotalSales;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class IndexModel : BasePageModel
{
    private readonly ITotalSaleApplication _application;

    public readonly DatabaseContext _context;

    public IndexModel(ITotalSaleApplication application, DatabaseContext context)
    {
        _application = application;
        _context = context;
        ViewModel = new List<DetailsViewModel>();
    }

    public IList<DetailsViewModel>  ViewModel { get; set; }

    public async Task OnGetAsync()
    {
        ViewModel = (await _application.GetAllTotalSale()).Data;
    }
}
