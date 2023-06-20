using Application.UserApp;
using Domain.Users;
using Framework.OperationResult;
using Framework.Password;
using ViewModels.Pages.Account;
using ViewModels.Pages.Admin.Users;

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

		public async Task<OperationResult> RegisterUser(RegisterViewModel model)
		{
			return await _userApplication.AddUser(new CreateViewModel
			{
				IsActive = true,
				Gender = model.Gender,
				Address = model.Address,
				FullName = model.FullName,
				Password = model.Password,
				Username = model?.Username,
				PostalCode = model.PostalCode,
				FatherName = model.FatherName,
				CityAddress = model.CityAddress,
				EmailAddress = model.EmailAddress,
				NationalCode = model.NationalCode,
				ProvinceAddress = model.ProvinceAddress,
				CellPhoneNumber = model?.CellPhoneNumber,
			});
		}

		public async Task<User> AuthenticateUser(LoginViewModel model)
		{
			var user = (await _userApplication?.GetUserByUserName(model?.Username)).Data;

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
