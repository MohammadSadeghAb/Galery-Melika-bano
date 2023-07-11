using Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Pages.Admin.ProductPics
{
    public class CommonViewModel
    {
        //**********
        [Display
            (Name = nameof(Resources.DataDictionary.Id),
            ResourceType = typeof(Resources.DataDictionary))]

        public Guid? Id { get; set; }
        //**********

        //**********
        [Display
            (Name = nameof(Resources.DataDictionary.InsertDateTime),
            ResourceType = typeof(Resources.DataDictionary))]

        public DateTime InsertDateTime { get; set; }
        //**********

        //**********
        [Display
            (Name = nameof(Resources.DataDictionary.UpdateDateTime),
            ResourceType = typeof(Resources.DataDictionary))]

        public DateTime UpdateDateTime { get; set; }
        //**********

        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        [Display
            (Name = nameof(Resources.DataDictionary.ProductName),
            ResourceType = typeof(Resources.DataDictionary))]

        public Guid Product_Id { get; set; }
        //**********

        //**********
        [Display
            (Name = nameof(Resources.DataDictionary.Picture),
            ResourceType = typeof(Resources.DataDictionary))]

        public string? PicAddress1 { get; set; }
        //**********

        //**********
        [Display
            (Name = nameof(Resources.DataDictionary.Picture),
            ResourceType = typeof(Resources.DataDictionary))]

        public string? PicAddress2 { get; set; }
        //**********

        //**********
        [Display
            (Name = nameof(Resources.DataDictionary.Picture),
            ResourceType = typeof(Resources.DataDictionary))]

        public string? PicAddress3 { get; set; }
        //**********

        //**********
        [Display
            (Name = nameof(Resources.DataDictionary.Picture),
            ResourceType = typeof(Resources.DataDictionary))]

        public string? PicAddress4 { get; set; }
        //**********

        //**********
        [Display
            (Name = nameof(Resources.DataDictionary.Picture),
            ResourceType = typeof(Resources.DataDictionary))]

        public string? PicAddress5 { get; set; }
        //**********
    }
}
