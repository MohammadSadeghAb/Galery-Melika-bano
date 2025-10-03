using Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class CommentRepository : RepositoryBase<Guid, Comment>, ICommentRepository
{
    private readonly DatabaseContext _context;

    public CommentRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<Comment>> GetAllComments()
    {
        return _context.Comments.Include(x => x.Product).Include(x => x.User).ToListAsync();
    }

    public Task<List<Comment>> GetAllProductComments(Guid productId)
    {
        return _context.Comments.Where(x => x.ProductId == productId).Include(x => x.Product).Include(x => x.User).ToListAsync();
    }

    public Task<List<Comment>> GetAllUserComments(Guid userId)
    {
        return _context.Comments.Where(x => x.UserId == userId).Include(x => x.Product).Include(x => x.User).ToListAsync();
    }
}
