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

namespace Server.Pages.Manager.Categories
{
    [Authorize(Roles = Constants.Role.Manager)]
    public class UpdateModel : BasePageModel
    {
        private readonly ICategoryApplication _application;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public UpdateModel(ICategoryApplication application, IWebHostEnvironment webHostEnvironment)
        {
            _application = application;
            _webHostEnvironment = webHostEnvironment;
            ParentsViewModel = new();
        }


        [BindProperty]
        public UpdateViewModel ViewModel { get; set; }
        public List<KeyValueViewModel> ParentsViewModel { get; set; }

        [BindProperty]
        [DataType(DataType.Upload)]
        public IFormFile UploadPic { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                AddToastError(Resources.Messages.Errors.IdIsNull);

                return RedirectToPage("Index");
            }

            ViewModel = (await _application.GetCategory(id.Value)).Data;

            if (ViewModel == null)
            {
                AddToastError(Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

                return RedirectToPage("Index");
            }

            if (ViewModel.ParentId.HasValue)
            {
                var parents = (await _application.GetParentCategories()).Data;

                foreach (var parent in parents)
                {
                    if (parent.Id != ViewModel.ParentId)
                    {
                        ParentsViewModel.Add(new KeyValueViewModel()
                        {
                            Id = parent.Id,
                            Name = parent.Name,
                        });
                    }
                }

                ParentsViewModel.Insert(0, new KeyValueViewModel()
                {
                    Id = ViewModel.ParentId,
                    Name = (await _application.GetCategory(ViewModel.ParentId.Value)).Data?.Name,
                });

                ParentsViewModel.Insert(1, new KeyValueViewModel()
                {
                    Id = null,
                    Name = DataDictionary.WithoutParent,
                });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Upload(UploadPic);

            var res = await _application.UpdateCategory(ViewModel);

            if (!res.Succeeded || res.ErrorMessages.Count > 0)
            {
                foreach (var error in res.ErrorMessages)
                {
                    AddToastError(error);
                }

                return RedirectToPage("Index");
            }

            var successMessage = string.Format(Successes.Updated, DataDictionary.Category);
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
