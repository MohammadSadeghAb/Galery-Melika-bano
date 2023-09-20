﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.AccountApp;
using Infrastructure;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Pages.Account;
using static Infrastructure.Constants;

namespace Server.Pages.Account
{
	public class LoginModel : BasePageModel
	{
		private readonly IAccountApplication _accountApplication;
		private readonly ApplicationSettings _applicationSettings;

		public LoginModel(IAccountApplication accountApplication, ApplicationSettings applicationSettings)
		{
			ViewModel = new();
			ViewModel.RememberMe = true;
			_accountApplication = accountApplication;
			_applicationSettings = applicationSettings;
		}

		[BindProperty]
		public string? ReturnUrl { get; set; }

		[BindProperty]
		public LoginViewModel ViewModel { get; set; }


		public void OnGet(string? returnUrl)
		{
			ReturnUrl = returnUrl;
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid == false)
			{
				return Page();
			}


			// **************************************************
			var user = new Domain.Users.User("Admin@domain.local");

			if ((ViewModel.Username == _applicationSettings.AdminUserPass.Username) &&
				 (ViewModel.Password == _applicationSettings.AdminUserPass.Password))
			{
				user = new Domain.Users.User("Admin@domain.local")
				{
					FullName = "مدیر سیستم",
					Username = "Admin",
					Role = Role.Admin
				};
			}
			else
			{
				user = await _accountApplication.AuthenticateUser(ViewModel);

				if (user == null)
				{
					AddPageError(Resources.Messages.Errors.CurrentPasswordIsNotCorrect);

					return Page();
				}
			}
			var claims = new List<Claim>();

			Claim claim;

			claim = GetUserClaims(user, claims);

			var claimsIdentity =
				new ClaimsIdentity(claims: claims,
				authenticationType: Infrastructure.Security.Utility.AuthenticationScheme);

			var claimsPrincipal =
				new ClaimsPrincipal(identity: claimsIdentity);

			var authenticationProperties =
				new AuthenticationProperties
				{
					IsPersistent = ViewModel.RememberMe,
				};

			await HttpContext.SignInAsync
				(scheme: Infrastructure.Security.Utility.AuthenticationScheme,
				principal: claimsPrincipal, properties: authenticationProperties);


			if (string.IsNullOrWhiteSpace(ReturnUrl))
			{
				return RedirectToPage(pageName: "/Index");
			}
			else
			{
				return Redirect(url: ReturnUrl);
			}
		}

		private static Claim GetUserClaims(Domain.Users.User user, List<Claim> claims)
		{
			// **************************************************
			Claim claim = new Claim
				(type: "FullName", value: string.Concat(user.FullName));
			claims.Add(item: claim);
			// **************************************************

			// **************************************************
			claim =
				new Claim
				(type: "Id", value: user.Id.ToString());

			claims.Add(item: claim);
			// **************************************************

			// **************************************************
			claim =
				new Claim
				(type: ClaimTypes.Role, value: user.Role ?? "User");

			claims.Add(item: claim);
			// **************************************************

			// **************************************************
			claim =
				new Claim
				(type: ClaimTypes.Name, value: user.Username);

			claims.Add(item: claim);
			// **************************************************

			// **************************************************
			claim =
				new Claim
				(type: ClaimTypes.Email, value: user.EmailAddress);

			claims.Add(item: claim);

			return claim;
		}
	}
}
