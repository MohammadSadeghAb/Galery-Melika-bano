using Application.UserApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Resources;
using System;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Users;

namespace Server.Pages.Account.User;

[Authorize]

public class UpdateModel : BasePageModel
{
    private readonly IUserApplication _application;

    public UpdateModel(IUserApplication application)
    {
        _application = application;
        ViewModel = new();
    }

    [BindProperty]
    public UpdateViewModel ViewModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Guid id = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

        if (id == null)
        {
            AddToastError(Resources.Messages.Errors.IdIsNull);

            return RedirectToPage("/Index");
        }

        ViewModel = (await _application.GetUser(id)).Data;

        if (ViewModel == null)
        {
            AddToastError(Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage("/Index");
        }

        AddPageWarning(Resources.Messages.Errors.Completeyourinformationbeforeordering);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        var res = await _application.UpdateUser(ViewModel);

        if (res.Succeeded == false || res.ErrorMessages.Count() > 0)
        {
            foreach (var item in res.ErrorMessages)
            {
                AddToastError(item);
            }

            return Page();
        }

        var successMessage = string.Format(Resources.Messages.Successes.Yourinformationhasbeeneditedsuccessfully);

        AddToastSuccess(successMessage);

        return RedirectToPage("/Index");
    }
}
