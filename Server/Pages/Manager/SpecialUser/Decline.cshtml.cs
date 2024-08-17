using Application.UserApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Resources;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Users;
using static System.Net.Mime.MediaTypeNames;

namespace Server.Pages.Manager.SpecialUser;

[Authorize(Roles = Infrastructure.Constants.Role.Manager)]

public class DeclineModel : BasePageModel
{
    public readonly IUserApplication _userApplication;

    public DeclineModel(IUserApplication userApplication)
    {
        _userApplication = userApplication;
        ViewModel = new UpdateViewModel();
    }

    [BindProperty]
    public UpdateViewModel ViewModel { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError(Resources.Messages.Errors.IdIsNull);

            return RedirectToPage("Index");
        }

        ViewModel = (await _userApplication.GetUser(id.Value)).Data;

        if (ViewModel == null)
        {
            AddToastError(Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage("Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        //if (ModelState.IsValid == false)
        //{
        //    return Page();
        //}

        var res = await _userApplication.DeclineToUser(ViewModel);

        if (res.Succeeded == false || res.ErrorMessages.Count > 0)
        {
            foreach (var item in res.ErrorMessages)
            {
                AddToastError(item);
            }

            return Page();
        }

        var successMessage = string.Format(Resources.Messages.Successes.Decline, DataDictionary.User);

        AddToastSuccess(successMessage);

        return RedirectToPage("Index");
    }
}
