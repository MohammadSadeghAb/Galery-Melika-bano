using Domain.SeedWork;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SaleAgg
{
    public class Sale : Entity
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

        public Guid? ProductId { get; set; }
        //**********

        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public int Number { get; set; }
        //**********

        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public int? Price { get; set; }
        //**********

    }
}
