namespace Pixogram.Post.Command.Infrastructure.Configurations;

public class MongoDbConfiguration
{
    public static string SectionName = "MongoDbConfig";

    public string ConnectionString { get; set; } = null!;
    public string Database { get; set; } = null!;
    public string Collection { get; set; } = null!;
}
