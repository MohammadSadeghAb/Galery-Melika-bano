using Domain.Users;
using Framework.OperationResult;
using ViewModels.Pages.Account;

namespace Application.AccountApp
{
	public interface IAccountApplication
	{
		Task<User> AuthenticateUser(LoginViewModel model);
		Task<OperationResult> RegisterUser(RegisterViewModel model);
		Task<OperationResult> UserRegister(UserRegistrationViewModel model);
	}
}