using Domain.SeedWork;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Pages.Admin.NEWS
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
        public string? PicName { get; set; }
        //**********

        //**********
        [MaxLength
        (length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        [Display
            (Name = nameof(Resources.DataDictionary.Title),
            ResourceType = typeof(Resources.DataDictionary))]

        public string Title { get; set; }
        //**********

        //**********
        [MaxLength
        (length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Display
            (Name = nameof(Resources.DataDictionary.Description),
            ResourceType = typeof(Resources.DataDictionary))]

        public string? Description { get; set; }
        //**********

        //**********
        [Display
            (Name = nameof(Resources.DataDictionary.IsActive),
            ResourceType = typeof(Resources.DataDictionary))]

        public bool IsActive { get; set; }
        //**********

        //**********
        [Range
            (minimum: 1, maximum: 100_000,
            ErrorMessageResourceType = typeof(Resources.Messages.Validations),
            ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]

        [Display
            (Name = nameof(Resources.DataDictionary.Ordering),
            ResourceType = typeof(Resources.DataDictionary))]

        public int Ordering { get; set; }
        //**********

        // **********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.InsertDateTime))]

        [DatabaseGenerated
            (DatabaseGeneratedOption.None)]
        public DateTime InsertDateTime { get; set; }
        // **********

        // **********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.UpdateDateTime))]

        [DatabaseGenerated
            (DatabaseGeneratedOption.None)]
        public DateTime UpdateDateTime { get; set; }
        // **********
    }
}
