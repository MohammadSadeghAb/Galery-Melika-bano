using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.Comments;

public class CommentViewModel : CreateCommentViewModel
{
    //**********
    public Guid Id { get; set; }
    //**********

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
