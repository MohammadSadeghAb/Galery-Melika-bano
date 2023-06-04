using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SaleAgg
{
    public interface ISaleRepository : IRepository<Guid, Sale>
    {
        Task<IList<Sale>> GetAllAsync();
    }
}
