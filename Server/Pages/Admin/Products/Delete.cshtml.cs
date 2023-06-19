using Application.CategoryApp;
using Application.ProductApp;
using Application.UserApp;
using Framework.OperationResult;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Products;

namespace Server.Pages.Admin.Products;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Admin)]

public class DeleteModel : Infrastructure.BasePageModel
{
    private readonly IProductApplication _application;

    private readonly ICategoryApplication _category;
    public DeleteModel(IProductApplication application,
                       ICategoryApplication category)
    {
        _category = category;
        _application = application;
    }

    [BindProperty]
    public DetailsViewModel ViewModel { get; set; }

    public OperationResultWithData<ViewModels.Pages.Admin.Categories.DetailsViewModel> category { get; set; }

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

        category = await _category.GetCategory(ViewModel.CategoryParent_Id);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id.HasValue == false)
        {
            AddToastError
                (message: Resources.Messages.Errors.IdIsNull);

            return RedirectToPage(pageName: "Index");
        }

        var res = (await _application.DeleteProduct(id.Value));

        if (res.Succeeded == false ||
            res.ErrorMessages.Count > 0)
        {
            foreach (var item in res.ErrorMessages)
            {
                AddToastError(item);
            }

            return Page();
        }

        var successMessage = string.Format
            (Resources.Messages.Successes.Deleted,
            Resources.DataDictionary.Product);

        AddToastSuccess(message: successMessage);

        return RedirectToPage(pageName: "Index");
    }
}
