using Pixogram.Post.Query.Application.Contracts.Handlers;
using Pixogram.Post.Query.Application.Queries;
using Pixogram.Post.Query.Domain.Contracts.Repositories;
using Pixogram.Post.Query.Domain.Entities;

namespace Pixogram.Post.Query.Application.Handlers;

public class QueryHandler : IQueryHandler
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;

    public QueryHandler(
        IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<PostEntity>> HandlerAsync(FindAllPostsQuery query)
    {
        var posts = await _postRepository.GetAllPostsAsync(query.Page, query.PageSize);
        return posts;
    }

    public Task<PostEntity> HandlerAsync(FindPostByIdQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PostEntity>> HandlerAsync(FindPostsByAuthorQuery query)
    {
        throw new NotImplementedException();
    }
}