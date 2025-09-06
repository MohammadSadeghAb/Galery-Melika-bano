using Domain.Users;
using Framework.OperationResult;
using ViewModels.Pages.Account;
using ViewModels.Pages.Admin.Users;

namespace Application.UserApp
{
	public interface IUserApplication
	{
		Task<User> AddUserAndRetuenId(UserRegistrationViewModel model);

        Task<OperationResult> AddUser(CreateViewModel user);
		Task<OperationResult> DeleteUser(Guid Id);
		Task<OperationResult> UpdateUser(UpdateViewModel user);
		Task<OperationResult> UpgradeToSpecial(UpdateViewModel user);
		Task<OperationResult> DeclineToUser(UpdateViewModel user);
		Task<OperationResultWithData<ViewModels.Pages.Admin.Users.DetailsViewModel>> GetUser(Guid Id);
		Task<OperationResultWithData<IList<ViewModels.Pages.Admin.Users.DetailsViewModel>>> GetAllUsers();
		Task<OperationResultWithData<Domain.Users.User>> GetUserByFullName(string Fullname);
	}
}
