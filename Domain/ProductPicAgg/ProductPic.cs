]using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductPicAgg
{
    public class ProductPic : Entity,IEntityHasUpdateDateTime
    {
        public ProductPic()
        {
            UpdateDateTime = InsertDateTime;
        }

        //**********
        public Guid Product_Id { get; set; }
        //**********

        //**********
        public DateTime UpdateDateTime { get; set; }
        //**********

        //**********
        public string PicAddress { get; set; }
        //**********

        public void SetUpdateDateTime()
        {
            UpdateDateTime = Utility.Now;
        }
    }
}
