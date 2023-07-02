using Application.ProductApp;
using Application.ProductPicApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.ProductPics;
using ViewModels.Shared;

namespace Server.Pages.Admin.Products;

[Authorize(Roles = Constants.Role.Admin)]

public class CreatePicModel : BasePageModel
{
    private readonly IProductPicApplication _application;

    private readonly IProductApplication _product;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public CreatePicModel(IProductPicApplication application,
                          IProductApplication product,
                          IWebHostEnvironment webHostEnvironment)
    {
        _product = product;
        _application = application;
        _webHostEnvironment = webHostEnvironment;
        products = new();
        ViewModel = new();
    }

    [BindProperty]
    public CommonViewModel ViewModel { get; set; }

    public List<KeyValueViewModel> products { get; set; }

    [BindProperty]
    [DataType(DataType.Upload)]
    public IFormFile UplodPic1 { get; set; }

    [BindProperty]
    [DataType(DataType.Upload)]
    public IFormFile UplodPic2 { get; set; }

    [BindProperty]
    [DataType(DataType.Upload)]
    public IFormFile UplodPic3 { get; set; }

    [BindProperty]
    [DataType(DataType.Upload)]
    public IFormFile UplodPic4 { get; set; }

    [BindProperty]
    [DataType(DataType.Upload)]
    public IFormFile UplodPic5 { get; set; }

    public async Task OnGetAsync()
    {
        var Products = (await _product.GetAllProduct()).Data;
        foreach (var item in Products)
        {
            products.Add(new KeyValueViewModel()
            {
                Id = item.Id,
                Name = item.Name_Product,
            });
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {

        //if (ModelState.Value)
        //{
        //    return Page();
        //}
        Upload1(UplodPic1);
        if (Upload2 != null)
        {
            Upload2(UplodPic2);
        }
        if (Upload3 != null)
        {
            Upload3(UplodPic3);
        }
        if (Upload4 != null)
        {
            Upload4(UplodPic4);
        }
        if (Upload5 != null)
        {
            Upload5(UplodPic5);
        }

        var res = await _application.AddProductPic(ViewModel);

        if (res.Succeeded == false || res.ErrorMessages.Count > 0)
        {
            foreach (var item in res.ErrorMessages)
            {
                AddToastError(item);
            }

            return Page();
        }

        var successMessage = string.Format(Resources.Messages.Successes.Created, DataDictionary.ProductPicture);

        AddToastSuccess(successMessage);

        return RedirectToPage("Index");
    }

    public void Upload1(IFormFile file)
    {
        var directoryPath = $"{Getwebroot()}\\ProductPic";
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        ViewModel.PicAddress1 = Path.Combine(Guid.NewGuid().ToString() + ".png");
        string filepath = Path.Combine(directoryPath, ViewModel.PicAddress1);
        using var output = System.IO.File.Create(filepath);
        file.CopyTo(output);
    }

    public void Upload2(IFormFile file)
    {
        var directoryPath = $"{Getwebroot()}\\ProductPic";
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        ViewModel.PicAddress2 = Path.Combine(Guid.NewGuid().ToString() + ".png");
        string filepath = Path.Combine(directoryPath, ViewModel.PicAddress2);
        using var output = System.IO.File.Create(filepath);
        file.CopyTo(output);
    }

    public void Upload3(IFormFile file)
    {
        var directoryPath = $"{Getwebroot()}\\ProductPic";
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        ViewModel.PicAddress3 = Path.Combine(Guid.NewGuid().ToString() + ".png");
        string filepath = Path.Combine(directoryPath, ViewModel.PicAddress3);
        using var output = System.IO.File.Create(filepath);
        file.CopyTo(output);
    }

    public void Upload4(IFormFile file)
    {
        var directoryPath = $"{Getwebroot()}\\ProductPic";
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        ViewModel.PicAddress4 = Path.Combine(Guid.NewGuid().ToString() + ".png");
        string filepath = Path.Combine(directoryPath, ViewModel.PicAddress4);
        using var output = System.IO.File.Create(filepath);
        file.CopyTo(output);
    }

    public void Upload5(IFormFile file)
    {
        var directoryPath = $"{Getwebroot()}\\ProductPic";
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        ViewModel.PicAddress5 = Path.Combine(Guid.NewGuid().ToString() + ".png");
        string filepath = Path.Combine(directoryPath, ViewModel.PicAddress5);
        using var output = System.IO.File.Create(filepath);
        file.CopyTo(output);
    }

    public string Getwebroot()
    {
        return _webHostEnvironment.WebRootPath;
    }
}
