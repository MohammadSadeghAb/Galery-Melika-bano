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

        public Guid Product_Id { get; set; }
        //**********

        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        [Display
            (Name = nameof(Resources.DataDictionary.Picture),
            ResourceType = typeof(Resources.DataDictionary))]

        public string PicAddress { get; set; }
        //**********
    }
}
