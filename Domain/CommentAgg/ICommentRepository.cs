namespace Domain.CommentAgg;

public interface ICommentRepository : IRepository<Guid, Comment>
{
    Task<List<Comment>> GetAllComments();
    Task<List<Comment>> GetAllProductComments(Guid productId);
    Task<List<Comment>> GetAllUserComments(Guid userId);
}
