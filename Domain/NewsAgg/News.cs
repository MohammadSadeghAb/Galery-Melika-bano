using Domain.SeedWork;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NewsAgg
{
    public class News : Entity, IEntityHasIsActive, IEntityHasUpdateDateTime
    {
        //**********
        public bool IsActive { get; set; }
        //**********

        // **********
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public DateTime UpdateDateTime { get; set; }
        // **********

        // **********
        public string? PicName { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Title,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public string Title { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Description,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        public string? Description { get; set; }
        // **********

        public void SetUpdateDateTime()
        {
            UpdateDateTime = Utility.Now;
        }
    }
}
