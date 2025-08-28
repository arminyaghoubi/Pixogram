namespace Pixogram.Post.Query.Domain.Entities;

public class PostEntity
{
    public Guid Id { get; set; }
    public string Author { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime DatePosted { get; set; }
    public int Likes { get; set; }
    public ICollection<CommentEntity>? Comments { get; set; }
}
