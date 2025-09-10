using Pixogram.Post.Query.Application.Contracts.Cache;
using Pixogram.Post.Query.Application.Contracts.Handlers;
using Pixogram.Post.Query.Application.Queries;
using Pixogram.Post.Query.Domain.Contracts.Repositories;
using Pixogram.Post.Query.Domain.Entities;

namespace Pixogram.Post.Query.Application.Handlers;

public class QueryHandler : IQueryHandler
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly ICacheService _cacheService;

    public QueryHandler(
        IPostRepository postRepository,
        ICommentRepository commentRepository,
        ICacheService cacheService)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<PostEntity>> HandlerAsync(FindAllPostsQuery query)
    {
        var cacheKey = $"{query.GetType().Name}_{query.Page}_{query.PageSize}";
        var posts = await _cacheService.GetValueAsync<IEnumerable<PostEntity>>(cacheKey);
        if (posts == null)
        {
            posts = await _postRepository.GetAllPostsAsync(query.Page, query.PageSize);
            await _cacheService.SetAsync(cacheKey, posts);
        }

        return posts;
    }

    public async Task<PostEntity?> HandlerAsync(FindPostByIdQuery query)
    {
        var cacheKey = $"{query.GetType().Name}_{query.PostId}";
        var post = await _cacheService.GetValueAsync<PostEntity>(cacheKey);
        if (post == null)
        {
            post = await _postRepository.GetPostByIdAsync(query.PostId);
            await _cacheService.SetAsync(cacheKey, post);
        }

        return post;
    }

    public async Task<IEnumerable<PostEntity>> HandlerAsync(FindPostsByAuthorQuery query)
    {
        var cacheKey = $"{query.GetType().Name}_{query.Author}";
        var posts = await _cacheService.GetValueAsync<IEnumerable<PostEntity>>(cacheKey);
        if (posts == null)
        {
            posts = await _postRepository.GetAllPostsFromAuthorAsync(query.Author);
            await _cacheService.SetAsync(cacheKey, posts);
        }

        return posts;
    }

    public async Task<IEnumerable<CommentEntity>> HandlerAsync(FindCommentsByPostIdQuery query)
    {
        var cacheKey = $"{query.GetType().Name}_{query.PostId}_{query.Page}_{query.PageSize}";
        var comments = await _cacheService.GetValueAsync<IEnumerable<CommentEntity>>(cacheKey);
        if (comments == null)
        {
            comments = await _commentRepository.GetAllCommentsFromPostAsync(query.PostId, query.Page, query.PageSize);
            await _cacheService.SetAsync(cacheKey, comments);
        }

        return comments;
    }
}