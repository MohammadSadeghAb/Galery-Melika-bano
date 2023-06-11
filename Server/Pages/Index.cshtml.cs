using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.ProductApp;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Resources.Messages;
using ViewModels.Pages.Admin.Products;

namespace Server.Pages
{
	public class IndexModel : BasePageModel
	{
		private readonly IProductApplication _application;

		public IndexModel(IProductApplication application,
						  DatabaseContext context)
		{
			_context = context;
			_application = application;
			ViewModel = new List<DetailsViewModel>();
		}

		public DatabaseContext _context { get; set; }

		public IList<DetailsViewModel> ViewModel { get; private set; }

		public async Task<IActionResult> OnGetAsync()
		{
			return Page();

			ViewModel = (await _application.GetAllProduct()).Data;

			//if (User == null || User.Identity == null || User.Identity.IsAuthenticated == false)
			//{
			//	return RedirectToPage("Account/Login");
			//}

			//var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

			//var fullName = User.Claims.FirstOrDefault(x => x.Type == "FullName")?.Value;

			//if (role == Constants.Role.User)
			//{
			//	AddToastSuccess(string.Format(Successes.Welcome, fullName));
			//	return RedirectToPage("User/Index");
			//}
			//else if (role == Constants.Role.Admin)
			//{
			//	AddToastSuccess(string.Format(Successes.Welcome, fullName));
			//	return RedirectToPage("Admin/Index");
			//}

			//return RedirectToPage("Account/Login");
		}
	}
}
