using Application.NewsApp;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Resources;
using Resources.Messages;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.NEWS;

namespace Server.Pages.Admin.the_News;

[Authorize(Roles = Constants.Role.Admin)]

public class CreateModel : BasePageModel
{
    private readonly INewsApplication _application;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public CreateModel(INewsApplication application, IWebHostEnvironment webHostEnvironment)
    {
        _application = application;
        _webHostEnvironment = webHostEnvironment;
        ViewModel = new();
    }

    //**********
    [BindProperty]
    public CommonViewModel ViewModel { get; set; }
    //**********

    //**********
    [BindProperty]
    [DataType(DataType.Upload)]
    [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]
    [Display
            (Name = nameof(Resources.DataDictionary.Picture),
            ResourceType = typeof(Resources.DataDictionary))]

    public IFormFile UplodPic { get; set; }
    //**********

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        Upload(UplodPic);

        var res = await _application.AddNews(ViewModel);

        if (res.Succeeded == false || res.ErrorMessages.Count > 0)
        {
            foreach (var item in res.ErrorMessages)
            {
                AddToastError(item);
            }

            return Page();
        }

        var successMessage = string.Format(Resources.Messages.Successes.Created, DataDictionary.Thenews);

        AddToastSuccess(successMessage);

        return RedirectToPage("Index");
    }

    public void Upload(IFormFile file)
    {
        var directoryPath = $"{Getwebroot()}\\NewsPic";
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        ViewModel.PicName = Path.Combine(Guid.NewGuid().ToString() + ".png");
        string filepath = Path.Combine(directoryPath, ViewModel.PicName);
        using var output = System.IO.File.Create(filepath);
        file.CopyTo(output);
    }

    public string Getwebroot()
    {
        return _webHostEnvironment.WebRootPath;
    }
}
