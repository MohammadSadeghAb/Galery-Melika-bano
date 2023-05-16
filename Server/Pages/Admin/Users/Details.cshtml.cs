using System;
using System.Threading.Tasks;
using Application.UserApp;
using Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ViewModels.Pages.Admin.Users;

namespace Server.Pages.Admin.Users;

[Microsoft.AspNetCore.Authorization.Authorize
	(Roles = Infrastructure.Constants.Role.Admin)]
public class DetailsModel : Infrastructure.BasePageModel
{
	public DetailsModel(ILogger<DetailsModel> logger,
		IUserApplication userApplication)
	{
		Logger = logger;
		UserApplication = userApplication;
		ViewModel = new();
	}

	// **********
	private ILogger<DetailsModel> Logger { get; }
	public IUserApplication UserApplication { get; }

	// **********

	// **********
	public DetailsViewModel ViewModel { get; private set; }
	// **********

	public async Task<IActionResult> OnGetAsync(Guid? id)
	{
		try
		{
			if (id.HasValue == false)
			{
				AddToastError
					(message: Resources.Messages.Errors.IdIsNull);

				return RedirectToPage(pageName: "Index");
			}

			ViewModel = (await UserApplication.GetUser(id.Value)).Data;

			if (ViewModel == null)
			{
				AddToastError
					(message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

				return RedirectToPage(pageName: "Index");
			}

			return Page();
		}
		catch (System.Exception ex)
		{
			Logger.LogError
				(message: Constants.Logger.ErrorMessage, args: ex.Message);

			AddToastError
				(message: Resources.Messages.Errors.UnexpectedError);

			return RedirectToPage(pageName: "Index");
		}

	}
}
