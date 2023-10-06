using Domain.ProductAgg;
using Domain.SeedWork;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TotalSaleAgg
{
    public class TotalSale : Entity
    {
        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public Guid UserId { get; set; }
        //**********

        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public Guid? Products { get; set; }
        //**********

        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public int? Number { get; set; }
        //**********

        //**********
        public string? PicAddress{ get; set; }
        //**********

        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public int? TotalPrice { get; set; }
        //**********

        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public int FactorNumber { get; set; }
        //**********

        //**********
        [MaxLength
        (length: Constants.FixedLength.TrackingCode,
        ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.MaxLength))]

        public string? TrackingCode { get; set; }
        //**********

        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public string Color { get; set; }
        //**********

        //**********
        public bool Accepted { get; set; }
        //**********

        //**********
        public bool Packing { get; set; }
        //**********

        //**********
        public bool Posted { get; set; }
        //**********

        //**********
        public bool Delivery { get; set; }
        //**********
    }
}
