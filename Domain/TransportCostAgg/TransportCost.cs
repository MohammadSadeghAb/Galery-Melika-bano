using Domain.SeedWork;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TransportCostAgg
{
    public class TransportCost : Entity, IEntityHasUpdateDateTime, IEntityHasIsActive
    {
        public TransportCost()
        {
            UpdateDateTime = InsertDateTime;
        }

        //**********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public int Maximum_Weight { get; set; }
        //**********

        // **********
        [Required
        (ErrorMessageResourceType = typeof(Validations),
        ErrorMessageResourceName = nameof(Validations.Required))]

        public int Price { get; set; }
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
}
