using Domain.NewsAgg;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class NewsRepository : RepositoryBase<Guid?, News>, INewsRepository
    {
        private readonly DatabaseContext _context;

        public NewsRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<News> GetByTitle(string Title)
        {
            return await _context.News.FirstOrDefaultAsync(x => x.Title == Title);
        }

        public async Task<IList<News>> GetAllAsync()
        {
            return await _context.News.OrderBy(x=> x.Ordering).ToListAsync();
        }
    }
}
