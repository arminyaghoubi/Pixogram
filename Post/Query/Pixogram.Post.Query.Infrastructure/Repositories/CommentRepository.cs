using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pixogram.Post.Query.Domain.Contracts.Repositories;
using Pixogram.Post.Query.Domain.Entities;
using Pixogram.Post.Query.Infrastructure.Contexts;

namespace Pixogram.Post.Query.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly IServiceScopeFactory _scopeFactory;

    public CommentRepository(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task CreateAsync(CommentEntity comment)
    {
        using var scope = _scopeFactory.CreateScope();
        var context= scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.AddAsync(comment);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid commentId)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var comment = await context.Comments.FindAsync(commentId);
        if (comment != null)
        {
            context.Remove(comment);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<CommentEntity>> GetAllCommentsFromPostAsync(Guid postId,int page,int pageSize)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var comments = await context.Comments
            .AsNoTracking()
            .Where(c => c.PostId == postId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return comments;
    }

    public async Task<CommentEntity?> GetCommentByIdAsync(Guid commentId)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var comment = await context.Comments
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == commentId);

        return comment;
    }

    public async Task UpdateAsync(CommentEntity comment)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Update(comment);
        await context.SaveChangesAsync();
    }
}