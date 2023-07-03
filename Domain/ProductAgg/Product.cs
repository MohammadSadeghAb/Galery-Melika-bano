using Domain.SeedWork;
using Domain.TotalSaleAgg;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductAgg
{
    public class Product : Entity, IEntityIdIsSetable, IEntityHasUpdateDateTime,IEntityHasIsActive
    {
        public Product()
        {
            UpdateDateTime = InsertDateTime;
        }

        // **********
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public DateTime UpdateDateTime { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Name,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public string Name_Product { get; set; }
        // **********

        // **********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public int Min_Major { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Color_Text,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public string Color1_text { get; set; }

        [MaxLength
        (length: Constants.MaxLength.Color_rgb,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public string Color1_rgb { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Color_Text,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        public string? Color2_text { get; set; }

        [MaxLength
        (length: Constants.MaxLength.Color_rgb,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        public string? Color2_rgb { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Color_Text,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        public string? Color3_text { get; set; }

        [MaxLength(length: Constants.MaxLength.Color_rgb,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        public string? Color3_rgb { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Color_Text,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        public string? Color4_text { get; set; }

        [MaxLength
        (length: Constants.MaxLength.Color_rgb,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        public string? Color4_rgb { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Gender,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        public string Gender { get; set; }
        // **********

        // **********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public double Number { get; set; }
        // **********

        // **********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public Guid CategoryParent_Id { get; set; }
        // **********

        // **********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public Guid CategoryChild_Id { get; set; }
        // **********

        // **********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public int Price { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Type,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        public string Type { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Price,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        public int? Discount_Single { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Price,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        public int? Discount_Major { get; set; }
        // **********

        // **********
        [MaxLength
        (length: Constants.MaxLength.Dimensions,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        public string? Dimensions { get; set; }
        // **********

        // **********
        public bool IsActive { get; set; }
        // **********

        //// **********
        //public Guid TotalSaleId { get; set; }
        //public TotalSale TotalSale { get; set; }
        //// **********

        public void SetId(Guid id)
        {
            Id = id;
        }

        public void SetUpdateDateTime()
        {
            UpdateDateTime = Utility.Now;
        }
    }
}
