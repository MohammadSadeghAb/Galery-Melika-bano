using Domain.ProductAgg;
using Domain.SeedWork;
using Domain.Users;
using Resources.Messages;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.Pages.Admin.Comments;
public class CreateCommentViewModel : object
{
	public CreateCommentViewModel() : base()
	{
	}

    // **********
    [System.ComponentModel.DataAnnotations.Display
        (Name = nameof(Resources.DataDictionary.IsActive),
        ResourceType = typeof(Resources.DataDictionary))]
    public bool IsActive { get; set; }
    // **********

    // **********
    [System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.Comment),
		ResourceType = typeof(Resources.DataDictionary))]

    [MaxLength
        (length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

    [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]
    public string Description { get; set; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.Score),
		ResourceType = typeof(Resources.DataDictionary))]
	public int Score { get; set; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.User),
		ResourceType = typeof(Resources.DataDictionary))]
	public Guid UserId { get; set; }
	public User? User { get; set; }
	// **********

	// **********
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.Product),
		ResourceType = typeof(Resources.DataDictionary))]
	public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    // **********
}
