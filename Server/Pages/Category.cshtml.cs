using Application.CategoryApp;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Categories;

namespace Server.Pages
{
    public class CategoryModel : BasePageModel
    {
        private readonly ICategoryApplication _category;

        public CategoryModel(ICategoryApplication category)
        {
            _category = category;
            ViewModelCategory = new List<IndexViewModel>();
        }

        public IList<IndexViewModel> ViewModelCategory { get; set; }

        public async Task OnGetAsync()
        {
            ViewModelCategory = (await _category.GetIndexCategories()).Data;
        }
    }
}
