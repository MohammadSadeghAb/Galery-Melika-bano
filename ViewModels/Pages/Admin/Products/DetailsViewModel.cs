using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ViewModels.Pages.Admin.Products
{
    public class DetailsViewModel : UpdateViewModel
    {
        // **********
        [Display
            (Name = nameof(Resources.DataDictionary.UpdateDateTime),
            ResourceType = typeof(Resources.DataDictionary))]
        public DateTime? UpdateDateTime { get; init; }
        // **********

        // **********
        [Display
            (Name = nameof(Resources.DataDictionary.InsertDateTime),
            ResourceType = typeof(Resources.DataDictionary))]
        public DateTime? InsertDateTime { get; init; }
        // **********
    }
}
