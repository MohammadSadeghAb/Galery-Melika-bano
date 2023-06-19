using Application.ProductApp;
using Application.ProductPicApp;
using Domain.CategoryAgg;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resources;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Products;
using ViewModels.Shared;

namespace Server.Pages.Admin.Products;

[Authorize(Roles = Constants.Role.Admin)]

public class CreateModel : BasePageModel
{
    private readonly IProductApplication _application;

    private readonly ICategoryRepository _category;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public CreateModel(IProductApplication application,
                       ICategoryRepository category,
                       IWebHostEnvironment webHostEnvironment)
    {
        _category = category;
        _application = application;
        ViewModel = new();
        categories = new();
    }

    [BindProperty]
    public CommonViewModel ViewModel { get; set; }

    public List<KeyValueViewModel> categories { get; set; }

    public async Task OnGetAsync()
    {
        var Categories = (await _category.GetParents());
        foreach (var category in Categories)
        {
            categories.Add(new KeyValueViewModel()
            {
                Id = category.Id,
                Name = category.Name,
            });
        }
    }

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
