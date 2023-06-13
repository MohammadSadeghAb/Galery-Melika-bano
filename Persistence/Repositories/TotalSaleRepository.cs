using Domain.TotalSaleAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class TotalSaleRepository : RepositoryBase<Guid, TotalSale>, ITotalSaleRepository
    {
        private readonly DatabaseContext _context;

        public TotalSaleRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<TotalSale>> GetAllAsync()
        {
            return await _context.TotalSales.ToListAsync();
        }
    }
}
