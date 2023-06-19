using Application.CategoryApp;
using Application.ProductApp;
using Application.UserApp;
using Framework.OperationResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Products;

namespace Server.Pages.Admin.Products;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class DetailsModel : Infrastructure.BasePageModel
{
    public DetailsModel(IProductApplication application,
                        ICategoryApplication category)
    {
        _catrgory = category;
        _application = application;
        ViewModel = new();
    }

    private readonly IProductApplication _application;

    private readonly ICategoryApplication _catrgory;

    public DetailsViewModel ViewModel { get; private set; }

    public OperationResultWithData<ViewModels.Pages.Admin.Categories.DetailsViewModel> categoty { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError
                (message: Resources.Messages.Errors.IdIsNull);

            return RedirectToPage(pageName: "Index");
        }

        ViewModel = (await _application.GetProduct(id.Value)).Data;

        if (ViewModel == null)
        {
            AddToastError
                (message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage(pageName: "Index");
        }

        categoty = await _catrgory.GetCategory(ViewModel.CategoryParent_Id);

        return Page();
    }
}
