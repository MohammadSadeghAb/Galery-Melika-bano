using Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Server.Pages.User
{
	[Authorize]
	public class IndexModel : BasePageModel
	{
		public void OnGet()
		{
		}
	}
}
