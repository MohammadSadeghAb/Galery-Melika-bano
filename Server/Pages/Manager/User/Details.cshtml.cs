using Application.UserApp;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Users;

namespace Server.Pages.Manager.User;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Manager)]

public class DetailsModel : BasePageModel
{
    private readonly IUserApplication _user;

    public DetailsModel(IUserApplication user)
    {
        _user = user;
    }

    public DetailsViewModel ViewModel { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError
                (message: Resources.Messages.Errors.IdIsNull);

            return RedirectToPage(pageName: "Index");
        }

        ViewModel = (await _user.GetUser(id.Value)).Data;

        if (ViewModel == null)
        {
            AddToastError
                (message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage(pageName: "Index");
        }

        return Page();
    }
}
