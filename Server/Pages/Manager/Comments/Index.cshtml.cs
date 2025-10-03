using Application.CommentApp;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Comments;

namespace Server.Pages.Manager.Comments;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class IndexModel : PageModel
{
    private readonly ICommentApplication _application;

    public IndexModel(ICommentApplication application)
    {
        _application = application;
    }

    public List<CommentViewModel> ViewModel { get; set; } = new();

    public async Task OnGet()
    {
        ViewModel = await _application.GetAllComments();
    }
}
