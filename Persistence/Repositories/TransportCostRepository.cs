using Domain;
using Domain.TransportCostAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class TransportCostRepository : RepositoryBase<Guid?, TransportCost> ,ITransportCostRepository
    {
        private readonly DatabaseContext _context;

        public TransportCostRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TransportCost> GetByWeight(int weight)
        {
            var Weight = await _context.TransportCosts.Where(x => x.Maximum_Weight >= weight).ToListAsync();
            return Weight.Min();
        }
        public async Task<IList<TransportCost>> GetAllAsync()
        {
            return await _context.TransportCosts.OrderBy(x => x.Maximum_Weight).ToListAsync();
        }
    }
}
