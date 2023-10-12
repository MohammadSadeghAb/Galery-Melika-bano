using Domain;
using Domain.TransportCostAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
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
            //int Price = (await _context.TransportCosts.FirstOrDefaultAsync(x => x.Maximum_Weight == Weight)).Price;
            if (Weight.Count == 0)
            {
                int max = await _context.TransportCosts.MaxAsync(x => x.Maximum_Weight);
                var maxweight = await _context.TransportCosts.FirstOrDefaultAsync(x => x.Maximum_Weight == max);
                return maxweight;
            }
            return Weight.MinBy(x => x.Maximum_Weight);
        }
        public async Task<IList<TransportCost>> GetAllAsync()
        {
            return await _context.TransportCosts.OrderBy(x => x.Maximum_Weight).ToListAsync();
        }
    }
}
