using CQRS.Core.Messages.Queries;
using Pixogram.Post.Query.Domain.Entities;

namespace Pixogram.Post.Query.Application.Queries;

public record FindCommentsByPostIdQuery(Guid PostId, int Page, int PageSize) : BaseQuery<IEnumerable<CommentEntity>>;
