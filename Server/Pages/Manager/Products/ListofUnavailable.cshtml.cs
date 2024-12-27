using Domain.ProductAgg;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Manager.Products;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Constants.Role.Manager)]

public class ListofUnavailableModel : BasePageModel
{
    public readonly DatabaseContext _context;

    public ListofUnavailableModel(DatabaseContext context)
    {
        _context = context;
        ViewModel = new List<Product>();
    }

    public IList<Product> ViewModel { get; set; }

    [BindProperty]
    public string Name { get; set; }

    public async Task OnGetAsync()
    {
        ViewModel = await _context.Products.Where(x => x.Number == 0).OrderBy(x => x.Name_Product).ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Name == null)
        {
            AddToastError(message: Resources.Messages.Errors.Pleaseenterthefactornumber);

            ViewModel = await _context.Products.Where(x => x.Number == 0).OrderBy(x => x.Name_Product).ToListAsync();

            return Page();
        }

        ViewModel = await _context.Products.Where(x => x.Name_Product.Contains(Name) && x.Number == 0).ToListAsync();

        return Page();
    }
}
