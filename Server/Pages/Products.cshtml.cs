using Application.ProductApp;
using Domain.ProductAgg;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Products;

namespace Server.Pages
{
    public class ProductsModel : BasePageModel
    {
        private readonly IProductApplication _product;

        public ProductsModel(IProductApplication product,
                             DatabaseContext context)
        {
            _product = product;
            _context = context;
            ViewModelProduct = new List<Product>();
        }

        public DatabaseContext _context { get; set; }

        public IList<Product> ViewModelProduct { get; set; }

        [BindProperty]
        public string Search { get; set; }

        public async Task OnGetAsync()
        {
            ViewModelProduct = await _context.Products.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Search == null)
            {
                ViewModelProduct = await _context.Products.Where(x => x.IsActive == true).ToListAsync();
            }

            if (Search != null)
            {
                ViewModelProduct = await _context.Products.Where(x => x.Name_Product.Contains($"{Search}") & x.IsActive == true).ToListAsync();
            }

            return Page();
        }
    }
}
