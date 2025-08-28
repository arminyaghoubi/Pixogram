using Pixogram.Post.Query.Domain.Entities;

namespace Pixogram.Post.Query.Domain.Contracts.Repositories;

public interface ICommentRepository
{
    Task CreateAsync(CommentEntity comment);
    Task UpdateAsync(CommentEntity comment);
    Task DeleteAsync(Guid commentId);
    Task<CommentEntity?> GetCommentByIdAsync(Guid commentId);
    Task<IEnumerable<CommentEntity>> GetAllCommentsFromPostAsync(Guid postId);
}
