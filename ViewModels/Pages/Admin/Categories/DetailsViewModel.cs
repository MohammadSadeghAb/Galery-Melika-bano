using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.Categories
{
    public class DetailsViewModel : UpdateViewModel
    {
        // **********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Parent))]
        public string? ParentName { get; set; }
        // **********

        // **********
        [Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.EditorUserId))]
        public string? EditorUserName { get; set; }
        // **********
    }
}