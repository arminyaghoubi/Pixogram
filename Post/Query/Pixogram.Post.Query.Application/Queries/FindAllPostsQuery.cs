using CQRS.Core.Messages.Queries;
using Pixogram.Post.Query.Domain.Entities;

namespace Pixogram.Post.Query.Application.Queries;

public record FindAllPostsQuery(int Page, int PageSize) : BaseQuery<IEnumerable<PostEntity>>;
