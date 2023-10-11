using Application.TransportCostApp;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.TransportCosts;

namespace Server.Pages.Admin.TransportCosts;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class IndexModel : BasePageModel
{
    private readonly ITransportCostApplication _application;

    public IndexModel(ITransportCostApplication application)
    {
        _application = application;
        ViewModel = new List<CommonViewModel>();
    }

    public IList<CommonViewModel> ViewModel { get; set; }

    public async Task OnGetAsync()
    {
        ViewModel = (await _application.GetAllTransportCost()).Data;
    }
}
