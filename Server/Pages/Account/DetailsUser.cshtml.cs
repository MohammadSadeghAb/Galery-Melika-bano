using Application.UserApp;
using Domain.CategoryAgg;
using Domain.Users;
using Framework.Password;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Users;

namespace Server.Pages.Account
{
    public class DetailsUserModel : BasePageModel
    {
        private readonly IUserApplication _application;

        public DetailsUserModel(IUserApplication application)
        {
            _application = application;
        }

        public Domain.Users.User ViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string fullname)
        {
            if (fullname == null)
            {
                AddToastError
                    (message: Resources.Messages.Errors.IdIsNull);

                return RedirectToPage(pageName: "Index");
            }

            ViewModel = (await _application.GetUserByFullName(fullname)).Data;

            if (ViewModel == null)
            {
                AddToastError
                    (message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

                return RedirectToPage(pageName: "Index");
            }

            return Page();
        }
    }
}
