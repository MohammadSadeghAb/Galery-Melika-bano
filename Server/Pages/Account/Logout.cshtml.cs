using Microsoft.AspNetCore.Authentication;

namespace Server.Pages.Account
{
	[Microsoft.AspNetCore.Authorization.Authorize]
	public class LogoutModel : Infrastructure.BasePageModel
	{
		public LogoutModel() : base()
		{
		}

		public async System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
		{
			// SignOutAsync -> using Microsoft.AspNetCore.Authentication;
			await HttpContext.SignOutAsync
				(scheme: Infrastructure.Security.Utility.AuthenticationScheme);

			AddToastSuccess(message: Resources.Messages.Successes.Youhaveloggedoutofyouraccount);
			return RedirectToPage(pageName: "/Index");
		}
	}
}
