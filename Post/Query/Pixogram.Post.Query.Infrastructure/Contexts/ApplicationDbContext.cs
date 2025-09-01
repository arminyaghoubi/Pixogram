using Microsoft.EntityFrameworkCore;
using Pixogram.Post.Query.Domain.Entities;
using System.Reflection;

namespace Pixogram.Post.Query.Infrastructure.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}
