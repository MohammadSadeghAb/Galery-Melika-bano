using Domain.ProductAgg;
using Domain.ProductPicAgg;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Xml.Linq;
using ViewModels.Shared;

namespace Server.Pages.Manager.Products;

[Microsoft.AspNetCore.Authorization.Authorize
    (Roles = Infrastructure.Constants.Role.Manager)]

public class DeletePicModel : BasePageModel
{
    private readonly DatabaseContext _context;

    public DeletePicModel(DatabaseContext context)
    {
        _context = context;
        products = new();
    }

    [BindProperty]
    [Display
        (Name = nameof(Resources.DataDictionary.ProductName),
        ResourceType = typeof(Resources.DataDictionary))]
    public Guid productid { get; set; }

    public List<KeyValueViewModel> products { get; set; }

    public async Task OnGetAsync()
    {
        var Products = (await _context.Products.OrderBy(x => x.Name_Product).ToListAsync());
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
        var deletepic = (await _context.ProductsPic.FirstOrDefaultAsync(x => x.Product_Id == productid));

        if (deletepic == null)
        {
            AddToastError
                (message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

            return RedirectToPage(pageName: "Index");
        }

        _context.Remove<ProductPic>(deletepic);
        await _context.SaveChangesAsync();

        var successMessage = string.Format
        (Resources.Messages.Successes.Deleted,
        Resources.DataDictionary.ProductPicture);

        AddToastSuccess(message: successMessage);

        return RedirectToPage(pageName: "Index");
    }
}
