using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UserApp;
using Microsoft.Extensions.Logging;
using ViewModels.Pages.Admin.Users;

namespace Server.Pages.Admin.Users;

[Microsoft.AspNetCore.Authorization.Authorize
	(Roles = Infrastructure.Constants.Role.Admin)]
public class IndexModel : Infrastructure.BasePageModel
{
	public IndexModel(ILogger<IndexModel> logger,
		IUserApplication userApplication)
	{
		Logger = logger;
		UserApplication = userApplication;
		ViewModel = new List<DetailsViewModel>();
	}

	private ILogger<IndexModel> Logger { get; }
	public IUserApplication UserApplication { get; }
	public IList<DetailsViewModel> ViewModel { get; private set; }


	public async Task OnGetAsync()
	{
		ViewModel = (await UserApplication.GetAllUsers()).Data;
	}
}
