using Application.AccountApp;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using ViewModels.Pages.Account;

namespace Server.Pages.Account
{
    public class UserRegistrationModel : BasePageModel
    {
        private readonly IAccountApplication _application;

        public UserRegistrationModel(IAccountApplication application)
        {
            _application = application;
            ViewModel = new();
        }

        [BindProperty]
        public UserRegistrationViewModel ViewModel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string username)
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var res = await _application.UserRegister(ViewModel);

            if (res.Succeeded == false || 0 < res.ErrorMessages.Count)
            {
                foreach (var error in res.ErrorMessages)
                {
                    AddPageError(error);
                }

                return Page();
            }

            return RedirectToPage("DetailsUser" , new { username = username });
        }
    }
}
