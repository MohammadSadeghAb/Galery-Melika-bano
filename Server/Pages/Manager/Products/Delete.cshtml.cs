using Application.ProductApp;
using Domain.CategoryAgg;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Products;

namespace Server.Pages.Manager.Products;

[Microsoft.AspNetCore.Authorization.Authorize
	(Roles = Infrastructure.Constants.Role.Manager)]

public class DeleteModel : BasePageModel
{
    private readonly IProductApplication _application;

    public DeleteModel(IProductApplication application)
    {
        _application = application;
    }

    public DetailsViewModel ViewModel { get; set; }

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
