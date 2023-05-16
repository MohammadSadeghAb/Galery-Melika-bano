using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductAgg
{
    public interface IProductRepository : IRepository<Guid, Product>
    {
        Task<IList<Product>> GetAllAsync();

        Task<Product> GetByName(string productname);
    }
}
