using Domain.Users;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Users;

namespace Server.Pages.Manager.User;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Constants.Role.Manager)]

public class IndexModel : BasePageModel
{
    private readonly DatabaseContext _context;

    public IndexModel(DatabaseContext context)
    {
        _context = context;
        ViewModel = new List<Domain.Users.User>();
    }

    [BindProperty]
    public string FullName { get; set; }

    public IList<Domain.Users.User> ViewModel { get; set; }

    public async Task OnGetAsync()
    {
        ViewModel = await _context.Users.Where(x => x.Role == Constants.Role.User).ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (FullName == null)
        {
            AddToastError(message: Resources.Messages.Errors.Pleaseenterthefactornumber);

            ViewModel = await _context.Users.Where(x => x.Role == Constants.Role.User).ToListAsync();

            return Page();
        }

        ViewModel = await _context.Users.Where(x => x.FullName.Contains(FullName) & x.Role == Constants.Role.User).ToListAsync();

        return Page();
    }
}
