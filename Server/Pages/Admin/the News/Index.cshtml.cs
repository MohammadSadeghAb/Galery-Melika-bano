using Application.NewsApp;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.NEWS;

namespace Server.Pages.Admin.the_News;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class IndexModel : BasePageModel
{
    private readonly INewsApplication _application;

    public IndexModel(INewsApplication application)
    {
        _application = application;
        ViewModel = new List<CommonViewModel>();
    }

    public IList<CommonViewModel> ViewModel { get; set; }

    public async Task OnGetAsync()
    {
        ViewModel = (await _application.GetAllNews()).Data;
    }
}
