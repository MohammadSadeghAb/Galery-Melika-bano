using Domain.SaleAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class SaleRepository : RepositoryBase<Guid, Sale>, ISaleRepository
    {
        private readonly DatabaseContext _context;

        public SaleRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<Sale>> GetAllAsync()
        {
            return await _context.Sales.ToListAsync();
        }
    }
}
