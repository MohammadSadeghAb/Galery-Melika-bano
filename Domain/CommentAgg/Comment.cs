using Domain.ProductAgg;
using Domain.SeedWork;
using Domain.Users;
using Resources.Messages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.CommentAgg;

public class Comment : Entity,
                       IEntityHasIsActive,
                       IEntityHasUpdateDateTime
{
    public Comment()
    {
        UpdateDateTime = InsertDateTime;
    }

    // **********
    [MaxLength
        (length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]
    public string Description { get; set; }
    // **********

    // **********
    public int Score { get; set; }
    // **********

    // **********
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    // **********

    // **********
    public Guid UserId { get; set; }
    public User User { get; set; }
    // **********

    // **********
    public bool IsActive { get; set; }
    // **********

    // **********
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public DateTime UpdateDateTime { get; set; }
    // **********

    public void SetUpdateDateTime()
    {
        UpdateDateTime = Utility.Now;
    }
}
