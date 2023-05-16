using Domain.ProductAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ProductRepository : RepositoryBase<Guid, Product>, IProductRepository
    {
        private readonly DatabaseContext _context;
        public ProductRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetByName(string productname)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Name_Product == productname);
        }

        public async Task<IList<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
