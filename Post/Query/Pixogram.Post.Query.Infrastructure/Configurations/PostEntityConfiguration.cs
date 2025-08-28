using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pixogram.Post.Query.Domain.Entities;

namespace Pixogram.Post.Query.Infrastructure.Configurations;

public class PostEntityConfiguration : IEntityTypeConfiguration<PostEntity>
{
    public void Configure(EntityTypeBuilder<PostEntity> builder)
    {
        builder.ToTable("Post");

        builder.HasMany(p=>p.Comments)
            .WithOne(c=>c.Post)
            .HasForeignKey(c=>c.PostId);
    }
}
