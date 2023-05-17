using Application.ProductApp;
using Application.UserApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Products;

namespace Server.Pages.Admin.Products;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class IndexModel : Infrastructure.BasePageModel
{
    public IndexModel(IProductApplication application)
    {
        _application = application;
        ViewModel = new List<DetailsViewModel>();
    }

    private readonly IProductApplication _application;

    public IList<DetailsViewModel> ViewModel { get; private set; }

    public async Task OnGetAsync()
    {
        ViewModel = (await _application.GetAllProduct()).Data;
    }
}
