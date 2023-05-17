using Application.ProductApp;
using Application.UserApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Resources;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Products;

namespace Server.Pages.Admin.Products;

[Authorize(Roles = Constants.Role.Admin)]

public class CreateModel : BasePageModel
{
    private readonly IProductApplication _application;
    public CreateModel(IProductApplication application)
    {
        _application = application;
        ViewModel = new();
    }

    [BindProperty]
    public CommonViewModel ViewModel { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        var res = await _application.AddProduct(ViewModel);

        if (res.Succeeded == false || res.ErrorMessages.Count > 0)
        {
            foreach (var item in res.ErrorMessages)
            {
                AddToastError(item);
            }

            return Page();
        }

        var successMessage = string.Format(Resources.Messages.Successes.Created, DataDictionary.Product);

        AddToastSuccess(successMessage);

        return RedirectToPage("Index");
    }
}
