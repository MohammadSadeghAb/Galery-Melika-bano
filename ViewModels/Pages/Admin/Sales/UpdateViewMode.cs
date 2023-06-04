using Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ViewModels.Pages.Admin.Sales
{
    public class UpdateViewMode
    {
        //**********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Id))]

        public Guid? Id { get; set; }
        //**********

        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Number))]

        public int Number { get; set; }
        //**********
    }
}
