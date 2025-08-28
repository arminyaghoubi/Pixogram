using Pixogram.Post.Query.Domain.Entities;

namespace Pixogram.Post.Query.Domain.Contracts.Repositories;

public interface IPostRepository
{
    Task CreateAsync(PostEntity post);
    Task UpdateAsync(PostEntity post);
    Task DeleteAsync(Guid postId);
    Task<PostEntity?> GetPostEntityAsync(Guid postId, bool withComment = false);
    Task<IEnumerable<PostEntity>> GetAllPostsAsync(int page, int pageSize);
    Task<IEnumerable<PostEntity>> GetAllPostsFromAuthorAsync(string author);
}
