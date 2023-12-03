using Application.ProductApp;
using Application.ProductPicApp;
using Application.UserApp;
using Domain.CategoryAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;
using Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Products;
using ViewModels.Shared;

namespace Server.Pages.Manager.Products;

[Authorize(Roles = Infrastructure.Constants.Role.Manager)]

public class UpdateModel : Infrastructure.BasePageModel
{
    public UpdateModel(IProductApplication application,
        IProductPicApplication productPicApplication,
        ICategoryRepository categoryRepository)
    {
        _application = application;
        _categoryRepository = categoryRepository;
        ViewModel = new();
        categories = new List<KeyValueViewModel>();
    }

    private readonly IProductApplication _application;

    private readonly ICategoryRepository _categoryRepository;

    [BindProperty]
    public UpdateViewModel ViewModel { get; set; }

    [BindProperty]
    public List<KeyValueViewModel> categories { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        var Categories = (await _categoryRepository.GetParents());
        foreach (var category in Categories)
        {
            categories.Add(new KeyValueViewModel()
            {
                Id = category.Id,
                Name = category.Name,
            });
        }

        if (id.HasValue == false)
        {
            AddToastError(Resources.Messages.Errors.IdIsNull);

            return RedirectToPage("Index");
        }

        ViewModel = (await _application.GetProduct(id.Value)).Data;

        if (ViewModel == null)
        {
            AddToastError(Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage("Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        var res = await _application.UpdateProduct(ViewModel);

        if (res.Succeeded == false || res.ErrorMessages.Count > 0)
        {
            foreach (var item in res.ErrorMessages)
            {
                AddToastError(item);
            }

            return Page();
        }

        var successMessage = string.Format(Resources.Messages.Successes.Updated, DataDictionary.Product);

        AddToastSuccess(successMessage);

        return RedirectToPage("Index");
    }
}
