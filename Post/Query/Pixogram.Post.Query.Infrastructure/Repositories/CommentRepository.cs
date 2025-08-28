using Microsoft.EntityFrameworkCore;
using Pixogram.Post.Query.Domain.Contracts.Repositories;
using Pixogram.Post.Query.Domain.Entities;
using Pixogram.Post.Query.Infrastructure.Contexts;

namespace Pixogram.Post.Query.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CommentEntity comment)
    {
        await _context.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid commentId)
    {
        var comment= await _context.Comments.FindAsync(commentId);
        if (comment != null)
        {
            _context.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<CommentEntity>> GetAllCommentsFromPostAsync(Guid postId)
    {
        var comments= await _context.Comments
            .AsNoTracking()
            .Where(c => c.PostId == postId)
            .ToListAsync();

        return comments;
    }

    public async Task UpdateAsync(CommentEntity comment)
    {
        _context.Update(comment);
        await _context.SaveChangesAsync();
    }
}