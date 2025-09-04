using CQRS.Core.Messages.Queries;
using Pixogram.Post.Query.Domain.Entities;

namespace Pixogram.Post.Query.Application.Queries;

public record FindPostByIdQuery(Guid PostId): BaseQuery<PostEntity>;
