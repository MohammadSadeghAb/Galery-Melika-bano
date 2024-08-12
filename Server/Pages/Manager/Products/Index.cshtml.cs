using Application.ProductApp;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Products;

namespace Server.Pages.Manager.Products;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Constants.Role.Manager)]

public class IndexModel : BasePageModel
{
    private readonly IProductApplication _application;

    public readonly DatabaseContext _context;

    public IndexModel(IProductApplication application,
                      DatabaseContext context)
    {
        _context = context;
        _application = application;
        ViewModel = new List<DetailsViewModel>();
    }

    public IList<DetailsViewModel> ViewModel { get; set; }

    public async Task OnGetAsync()
    {
        ViewModel = (await _application.GetAllProduct()).Data;
    }
}
