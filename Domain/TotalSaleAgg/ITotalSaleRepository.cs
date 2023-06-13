using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TotalSaleAgg
{
    public interface ITotalSaleRepository : IRepository<Guid, TotalSale>
    {
        Task<IList<TotalSale>> GetAllAsync();
    }
}
