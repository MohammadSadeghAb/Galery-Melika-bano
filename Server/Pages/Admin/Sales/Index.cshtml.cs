using Application.SaleApp;
using Infrastructure;
using Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Sales;

namespace Server.Pages.Admin.Sales;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class IndexModel : BasePageModel
{
    private readonly ISaleApplication _application;

    public IndexModel(ISaleApplication application, DatabaseContext context)
    {
        _context = context;
        _application = application;
        ViewModel = new List<DetailsViewModel>();
    }

    public IList<DetailsViewModel> ViewModel { get; private set; }

    public DatabaseContext _context { get; set; }

    public async Task OnGetAsync()
    {
        ViewModel = (await _application.GetAllSale()).Data;
    }
}
