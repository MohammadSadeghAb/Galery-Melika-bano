using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.CategoryApp;
using Application.ProductApp;
using Domain.ProductAgg;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Resources.Messages;
using ViewModels.Pages.Admin.Products;

namespace Server.Pages
{
	public class IndexModel : BasePageModel
	{
		private readonly IProductApplication _product;

		private readonly ICategoryApplication _category;

		public IndexModel(IProductApplication product,
						  ICategoryApplication category,
						  DatabaseContext context)
		{
			_context = context;
			_product = product;
			_category = category;
            ViewModelProduct = new List<Product>();
			ViewModelCategory = new List<ViewModels.Pages.Admin.Categories.IndexViewModel>();
		}

		public DatabaseContext _context { get; set; }

		public IList<Product> ViewModelProduct { get; private set; }

        public IList<DetailsViewModel> ViewModelProductNewset { get; set; }

        public IList<ViewModels.Pages.Admin.Categories.IndexViewModel> ViewModelCategory { get; set; }

		[BindProperty]
		public string Search { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{

			ViewModelProduct = await _context.Products.Where(x => x.IsActive == true).ToListAsync();

			ViewModelCategory = (await _category.GetIndexCategories()).Data;

			ViewModelProductNewset = (await _product.Getnewest()).Data;

			return Page();
		}

        public async Task<IActionResult> OnPostAsync()
        {
            if (Search == null)
            {
                ViewModelProduct = await _context.Products.ToListAsync();
                ViewModelCategory = (await _category.GetIndexCategories()).Data;
                ViewModelProductNewset = (await _product.Getnewest()).Data;
            }

            if (Search != null)
            {
                ViewModelProduct = await _context.Products.Where(x => x.Name_Product.Contains($"{Search}") & x.IsActive == true).ToListAsync();
                ViewModelCategory = (await _category.GetIndexCategories()).Data;
                ViewModelProductNewset = (await _product.Getnewest()).Data;
            }

            return Page();
        }
    }
}
