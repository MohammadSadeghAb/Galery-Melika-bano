using Application.ProductApp;
using Domain.ProductAgg;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Parbad.Internal;
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
        ViewModel = new List<Product>();
    }

    public IList<Product> ViewModel { get; set; }

    [BindProperty]
    public string Name { get; set; }

    public async Task OnGetAsync()
    {
        ViewModel = await _context.Products.OrderBy(x => x.Name_Product).ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Name == null)
        {
            AddToastError(message: Resources.Messages.Errors.Pleaseenterthefactornumber);

            ViewModel = await _context.Products.OrderBy(x => x.Name_Product).ToListAsync();

            return Page();
        }

        ViewModel = await _context.Products.Where(x => x.Name_Product.Contains(Name)).ToListAsync();

        return Page();
    }

}
