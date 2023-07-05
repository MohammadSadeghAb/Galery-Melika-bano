using Domain.Users;
using Framework.OperationResult;
using ViewModels.Pages.Admin.Users;

namespace Application.UserApp
{
	public interface IUserApplication
	{
		Task<OperationResult> AddUser(CreateViewModel user);
		Task<OperationResult> DeleteUser(Guid Id);
		Task<OperationResult> UpdateUser(UpdateViewModel user);
		Task<OperationResultWithData<ViewModels.Pages.Admin.Users.DetailsViewModel>> GetUser(Guid Id);
		Task<OperationResultWithData<IList<ViewModels.Pages.Admin.Users.DetailsViewModel>>> GetAllUsers();
		Task<OperationResultWithData<Domain.Users.User>> GetUserByUserName(string username);
	}
}
