using Domain.CommentAgg;
using Domain.Users;
using ViewModels.Pages.Admin.Comments;
using static System.Formats.Asn1.AsnWriter;

namespace Application.CommentApp;

public class CommentApplication : ICommentApplication
{
    private readonly ICommentRepository _commentRepository;
    public CommentApplication(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task AddComment(CreateCommentViewModel model)
    {
        var comment = new Comment
        {
            Score = model.Score,
            UserId = model.UserId,
            IsActive = model.IsActive,
            ProductId = model.ProductId,
            Description = model.Description,
        };

        await _commentRepository.CreateAsync(comment);

        await _commentRepository.SaveChangesAsync();
    }

    public async Task DeleteComment(Guid Id)
    {
        var comment = await _commentRepository.GetAsync(Id);

        _commentRepository.Remove(comment);

        await _commentRepository.SaveChangesAsync();
    }

    public async Task<List<CommentViewModel>> GetAllComments()
    {
        var comments = new List<CommentViewModel>();
        var data = await _commentRepository.GetAllComments();

        foreach (var model in data)
        {
            comments.Add(new CommentViewModel
            {
                Id = model.Id,
                User = model.User,
                Score = model.Score,
                UserId = model.UserId,
                Product = model.Product,
                IsActive = model.IsActive,
                ProductId = model.ProductId,
                Description = model.Description,
                InsertDateTime = model.InsertDateTime,
                UpdateDateTime = model.UpdateDateTime,
            });
        }

        return comments;
    }

    public async Task<List<CommentViewModel>> GetAllProductComments(Guid productId)
    {
        var comments = new List<CommentViewModel>();
        var data = await _commentRepository.GetAllProductComments(productId);

        foreach (var model in data)
        {
            comments.Add(new CommentViewModel
            {
                Id = model.Id,
                User = model.User,
                Score = model.Score,
                UserId = model.UserId,
                Product = model.Product,
                IsActive = model.IsActive,
                ProductId = model.ProductId,
                Description = model.Description,
                InsertDateTime = model.InsertDateTime,
                UpdateDateTime = model.UpdateDateTime,
            });
        }

        return comments;
    }

    public async Task<List<CommentViewModel>> GetAllUserComments(Guid userId)
    {
        var comments = new List<CommentViewModel>();
        var data = await _commentRepository.GetAllUserComments(userId);

        foreach (var model in data)
        {
            comments.Add(new CommentViewModel
            {
                Id = model.Id,
                User = model.User,
                Score = model.Score,
                UserId = model.UserId,
                Product = model.Product,
                IsActive = model.IsActive,
                ProductId = model.ProductId,
                Description = model.Description,
                InsertDateTime = model.InsertDateTime,
                UpdateDateTime = model.UpdateDateTime,
            });
        }

        return comments;
    }

    public async Task<CommentViewModel> GetComment(Guid Id)
    {
        var comment = await _commentRepository.GetAsync(Id);

        var _comment = new CommentViewModel
        {
            Id = comment.Id,
            Score = comment.Score,
            UserId = comment.UserId,
            IsActive = comment.IsActive,
            ProductId = comment.ProductId,
            Description = comment.Description,
            InsertDateTime = comment.InsertDateTime,
            UpdateDateTime = comment.UpdateDateTime,
        };

        return _comment;
    }

    public async Task UpdateComment(CommentViewModel model)
    {
        var comment = await _commentRepository.GetAsync(model.Id);

        comment.Score = model.Score;
        comment.UserId = model.UserId;
        comment.IsActive = model.IsActive;
        comment.ProductId = model.ProductId;
        comment.Description = model.Description;
        comment.SetUpdateDateTime();

        await _commentRepository.SaveChangesAsync();
    }
}
