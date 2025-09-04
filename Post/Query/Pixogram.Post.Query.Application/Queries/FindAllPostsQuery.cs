using CQRS.Core.Messages.Queries;
using Pixogram.Post.Query.Domain.Entities;

namespace Pixogram.Post.Query.Application.Queries;

public class FindAllPostsQuery : BaseQuery<IEnumerable<PostEntity>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}
