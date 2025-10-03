using Application.CommentApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Comments;

namespace Server.Pages.User.Comments;

[Authorize]

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
        Guid userId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

        ViewModel = await _application.GetAllUserComments(userId);
    }
}
