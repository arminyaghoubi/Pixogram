using Microsoft.EntityFrameworkCore;
using Pixogram.Post.Query.Domain.Contracts.Repositories;
using Pixogram.Post.Query.Domain.Entities;
using Pixogram.Post.Query.Infrastructure.Contexts;

namespace Pixogram.Post.Query.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _context;

    public PostRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(PostEntity post)
    {
        await _context.AddAsync(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid postId)
    {
        var post = await _context.Posts.FindAsync(postId);
        if (post == null)
            return;

        _context.Remove(post);
        await _context.SaveChangesAsync();

    }

    public async Task<IEnumerable<PostEntity>> GetAllPostsAsync(int page, int pageSize)
    {
        var posts = await _context.Posts
            .AsNoTracking()
            .OrderByDescending(p => p.DatePosted)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return posts;
    }

    public async Task<IEnumerable<PostEntity>> GetAllPostsFromAuthorAsync(string author)
    {
        var posts = await _context.Posts
            .AsNoTracking()
            .Where(p => p.Author == author)
            .OrderByDescending(p => p.DatePosted)
            .ToListAsync();

        return posts;
    }

    public async Task<PostEntity?> GetPostEntityAsync(Guid postId, bool withComment = false)
    {
        var query= _context.Posts.AsNoTracking();
        if (withComment)
            query = query.Include(p => p.Comments);

        var post = await query.FirstOrDefaultAsync(p => p.Id == postId);
        return post;
    }

    public async Task UpdateAsync(PostEntity post)
    {
        _context.Update(post);
        await _context.SaveChangesAsync();
    }
}
