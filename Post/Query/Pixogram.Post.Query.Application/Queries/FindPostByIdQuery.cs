using CQRS.Core.Messages.Queries;
using Pixogram.Post.Query.Domain.Entities;

namespace Pixogram.Post.Query.Application.Queries;

public class FindPostByIdQuery: BaseQuery<PostEntity>
{
    public Guid PostId { get; set; }
}
