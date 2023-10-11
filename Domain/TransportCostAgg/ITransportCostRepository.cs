using Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TransportCostAgg
{
    public interface ITransportCostRepository : IRepository<Guid?, TransportCost>
    {
        Task<IList<TransportCost>> GetAllAsync();

        Task<TransportCost> GetByWeight(int weight);
    }
}
