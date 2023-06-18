using Application.CategoryApp;
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
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Categories;
using ViewModels.Shared;

namespace Server.Pages.Admin.Categories
{
    [Authorize(Roles = Constants.Role.Admin)]
    public class CreateModel : BasePageModel
    {
        private readonly ICategoryApplication _application;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(ICategoryApplication application, IWebHostEnvironment webHostEnvironment)
        {
            _application = application;
            _webHostEnvironment = webHostEnvironment;
            ViewModel = new();
            ParentsViewModel = new();
        }


        [BindProperty]
        public CreateViewModel ViewModel { get; set; }
        public List<KeyValueViewModel> ParentsViewModel { get; set; }

        [BindProperty]
        [DataType(DataType.Upload)]
        public IFormFile UploadPic { get; set; }

        public async Task Start()
        {
            var parents = (await _application.GetParentCategories()).Data;
            foreach (var parent in parents)
            {
                ParentsViewModel.Add(new KeyValueViewModel()
                {
                    Id = parent.Id,
                    Name = parent.Name,
                });
            }
        }

        public async Task OnGetAsync()
        {
            await Start();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Upload(UploadPic);

            var res = await _application.AddCategory(ViewModel);

            if (!res.Succeeded || res.ErrorMessages.Count > 0)
            {
                foreach (var error in res.ErrorMessages)
                {
                    AddPageError(error);
                }

                await Start();
                return Page();
            }

            var successMessage = string.Format(Successes.Created, DataDictionary.Category);
            AddToastSuccess(successMessage);

            return RedirectToPage("Index");
        }

        public void Upload(IFormFile file)
        {
            var directoryPath = $"{Getwebroot()}\\CategoryPic";
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string filename = Path.Combine(ViewModel.Name + ".png");
            string filepath = Path.Combine(directoryPath, filename);
            using var output = System.IO.File.Create(filepath);
            file.CopyTo(output);
        }

        public string Getwebroot()
        {
            return _webHostEnvironment.WebRootPath;
        }

    }
}
