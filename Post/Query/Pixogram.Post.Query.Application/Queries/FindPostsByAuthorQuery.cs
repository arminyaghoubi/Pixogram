using CQRS.Core.Messages.Queries;
using Pixogram.Post.Query.Domain.Entities;

namespace Pixogram.Post.Query.Application.Queries;

public class FindPostsByAuthorQuery: BaseQuery<IEnumerable<PostEntity>>
{
    public string Author { get; set; } = null!;
}
