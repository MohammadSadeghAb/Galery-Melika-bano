using Application.TransportCostApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Resources;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.TransportCosts;

namespace Server.Pages.Admin.TransportCosts;

[Authorize(Roles = Constants.Role.Admin)]

public class CreateModel : BasePageModel
{
    private readonly ITransportCostApplication _application;

    public CreateModel(ITransportCostApplication application)
    {
        _application = application;
        ViewModel = new();
    }

    [BindProperty]
    public CommonViewModel ViewModel { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        var res = await _application.AddTransportCost(ViewModel);

        if (res.Succeeded == false || res.ErrorMessages.Count > 0)
        {
            foreach (var item in res.ErrorMessages)
            {
                AddToastError(item);
            }

            return Page();
        }

        var successMessage = string.Format(Resources.Messages.Successes.Created, DataDictionary.ShippingCost);

        AddToastSuccess(successMessage);

        return RedirectToPage("Index");
    }
}
