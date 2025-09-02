using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pixogram.Post.Query.Domain.Contracts.Repositories;
using Pixogram.Post.Query.Domain.Entities;
using Pixogram.Post.Query.Infrastructure.Contexts;

namespace Pixogram.Post.Query.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly IServiceScopeFactory _scopeFactory;

    public PostRepository(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task CreateAsync(PostEntity post)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.AddAsync(post);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid postId)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var post = await context.Posts.FindAsync(postId);
        if (post == null)
            return;

        context.Remove(post);
        await context.SaveChangesAsync();

    }

    public async Task<IEnumerable<PostEntity>> GetAllPostsAsync(int page, int pageSize)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var posts = await context.Posts
            .AsNoTracking()
            .OrderByDescending(p => p.DatePosted)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return posts;
    }

    public async Task<IEnumerable<PostEntity>> GetAllPostsFromAuthorAsync(string author)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var posts = await context.Posts
            .AsNoTracking()
            .Where(p => p.Author == author)
            .OrderByDescending(p => p.DatePosted)
            .ToListAsync();

        return posts;
    }

    public async Task<PostEntity?> GetPostByIdAsync(Guid postId, bool withComment = false)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var query= context.Posts.AsNoTracking();
        if (withComment)
            query = query.Include(p => p.Comments);

        var post = await query.FirstOrDefaultAsync(p => p.Id == postId);
        return post;
    }

    public async Task UpdateAsync(PostEntity post)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Update(post);
        await context.SaveChangesAsync();
    }
}
