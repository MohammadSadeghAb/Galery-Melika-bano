using Domain.CategoryAgg;
using Domain.ProductAgg;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages;

public class CategoryProductsModel : BasePageModel
{
    public readonly DatabaseContext _context;

    public CategoryProductsModel(DatabaseContext context)
    {
        _context = context;
        ViewModelProducts = new List<Product>();
    }

    public IList<Product> ViewModelProducts { get; set; }

    public Category category { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if(id.Value == null)
        {
            AddToastError(message: Resources.Messages.Errors.IdIsNull);
            return RedirectToPage("/Index");
        }

        category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id.Value);
        ViewModelProducts = await _context.Products.Where(x => x.CategoryParent_Id == id.Value).ToListAsync();

        return Page();
    }
}
