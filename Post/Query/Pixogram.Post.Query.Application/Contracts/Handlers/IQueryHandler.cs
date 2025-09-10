using Pixogram.Post.Query.Application.Queries;
using Pixogram.Post.Query.Domain.Entities;

namespace Pixogram.Post.Query.Application.Contracts.Handlers;

public interface IQueryHandler
{
    Task<IEnumerable<PostEntity>> HandlerAsync(FindAllPostsQuery query);
    Task<PostEntity?> HandlerAsync(FindPostByIdQuery query);
    Task<IEnumerable<PostEntity>> HandlerAsync(FindPostsByAuthorQuery query);
    Task<IEnumerable<CommentEntity>> HandlerAsync(FindCommentsByPostIdQuery query);
}
