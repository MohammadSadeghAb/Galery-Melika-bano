using Domain.SeedWork;
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
        public string PicAddress1 { get; set; }
        //**********

        //**********
        public string PicAddress2 { get; set; }
        //**********

        //**********
        public string PicAddress3 { get; set; }
        //**********

        //**********
        public string PicAddress4 { get; set; }
        //**********

        //**********
        public string PicAddress5 { get; set; }
        //**********

        public void SetUpdateDateTime()
        {
            UpdateDateTime = Utility.Now;
        }
    }
}
