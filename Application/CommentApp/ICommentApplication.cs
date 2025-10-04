using ViewModels.Pages.Admin.Comments;

namespace Application.CommentApp;

public interface ICommentApplication
{
    Task AddComment(CreateCommentViewModel model);
    Task UpdateComment(CommentViewModel model);
    Task DeleteComment(Guid Id);
    Task<CommentViewModel> GetComment(Guid Id);
    Task<List<CommentViewModel>> GetAllComments();
    Task<List<CommentViewModel>> GetAllProductComments(Guid productId);
    Task<List<CommentViewModel>> GetAllUserComments(Guid userId);
}
