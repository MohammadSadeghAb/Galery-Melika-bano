using Domain.ProductPicAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ProductPicRepository : RepositoryBase<Guid, ProductPic>, IProductPicRepository
    {
        private readonly DatabaseContext _context;
        public ProductPicRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<ProductPic>> GetAllAsync()
        {
            return await _context.ProductsPic.ToListAsync();
        }
    }
}
