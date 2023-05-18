using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductPicAgg
{
    public interface IProductPicRepository : IRepository<Guid, ProductPic>
    {
        Task<IList<ProductPic>> GetAllAsync();
    }
}
