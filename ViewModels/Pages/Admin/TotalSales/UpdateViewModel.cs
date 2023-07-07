using Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ViewModels.Pages.Admin.TotalSales
{
    public class UpdateViewModel
    {
        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Id))]

        public Guid? Id { get; set; }
        //**********

        //**********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Accepted))]

        public bool Accepted { get; set; }
        //**********

        //**********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Packing))]

        public bool Packing { get; set; }
        //**********

        //**********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Posted))]

        public bool Posted { get; set; }
        //**********

        //**********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Delivery))]

        public bool Delivery { get; set; }
        //**********
    }
}
