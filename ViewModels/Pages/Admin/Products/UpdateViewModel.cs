using Domain.SeedWork;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Pages.Admin.Products
{
    public class UpdateViewModel : CommonViewModel
    {
        // **********
        [MaxLength
        (length: Constants.MaxLength.Color_Text,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Color2))]

        public string? Color2_text { get; set; }

        [MaxLength
        (length: Constants.MaxLength.Color_rgb,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Color2))]

        public string? Color2_rgb { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Color_Text,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Color3))]

        public string? Color3_text { get; set; }

        [MaxLength(length: Constants.MaxLength.Color_rgb,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Color3))]

        public string? Color3_rgb { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Color_Text,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Color4))]

        public string? Color4_text { get; set; }

        [MaxLength
        (length: Constants.MaxLength.Color_rgb,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Color4))]

        public string? Color4_rgb { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Dimensions,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Dimensions))]

        public string? Dimensions { get; set; }
        // **********

    }
}
