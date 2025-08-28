namespace Pixogram.Post.Query.Domain.Entities;

public class CommentEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool IsEdited { get; set; }
    public DateTime CommentDate { get; set; }

    public Guid PostId { get; set; }
    public PostEntity Post { get; set; } = null!;
}