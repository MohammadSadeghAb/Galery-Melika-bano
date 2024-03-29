﻿using Application.UserApp;
using Domain.Users;
using Framework.OperationResult;
using Framework.Password;
using ViewModels.Pages.Account;
using ViewModels.Pages.Admin.Users;
using User = Domain.Users.User;

namespace Application.AccountApp
{
	public class AccountApplication : IAccountApplication
	{
		private readonly IUserApplication _userApplication;
		private readonly IPasswordHasher _hasher;

		public AccountApplication(IUserApplication userApplication, IPasswordHasher hasher)
		{
			_userApplication = userApplication;
			_hasher = hasher;
		}

		public async Task<OperationResult> UserRegister(UserRegistrationViewModel model)
		{
            return await _userApplication.AddUser(new CreateViewModel
            {
                Role = "User",
                IsActive = true,
				Gender = model.Gender,
				Address = model.Address,
				CityAddress = model.City,
                FullName = model.FullName,
                Password = model.Password,
                Username = model?.Username,
				PostalCode = model.PostalCode,
				ProvinceAddress = model.Province,
				NationalCode = model.NationalCode,
				//EmailAddress = model.EmailAddress,
				CellPhoneNumber = model.CellPhoneNumber,
            });
        }


        public async Task<OperationResult> RegisterUser(RegisterViewModel model)
		{
			return await _userApplication.AddUser(new CreateViewModel
			{
				Role = "User",
				IsActive = true,
				FullName = model.FullName,
				Password = model.Password,
				Username = model.FullName,
			});
		}

		public async Task<User> AuthenticateUser(LoginViewModel model)
		{
			var user = (await _userApplication?.GetUserByFullName(model?.FullName)).Data;

			if (user != null)
			{
				var passCheck = _hasher.Check(user.Password, model.Password);

				if (passCheck.Verified)
				{
					return user;
				}
			}

			return null;
		}
	}
}
