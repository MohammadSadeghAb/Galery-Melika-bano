using Resources.Messages;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.Sales
{
    public class CommonViewModel
    {
        //**********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Id))]

        public Guid? Id { get; set; }
        //**********

        //**********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.InsertDateTime))]

        public DateTime? InsertDateTime { get; set; }
        //**********

        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.User))]

        public Guid UserId { get; set; }
        //**********

        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.ProductName))]

        public Guid ProductId { get; set; }
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

        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Number))]

        public int Price { get; set; }
        //**********
    }
}
