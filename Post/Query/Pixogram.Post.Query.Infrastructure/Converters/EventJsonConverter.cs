using CQRS.Core.Messages.Events;
using Pixogram.Post.Common.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pixogram.Post.Query.Infrastructure.Converters;

public class EventJsonConverter : JsonConverter<BaseEvent>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableFrom(typeof(BaseEvent));
    }

    public override BaseEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader,out var document))
            throw new JsonException("Invalid json structure.");
        
        if(!document.RootElement.TryGetProperty("Type",out var typeValue))        
            throw new JsonException("Type property dose not exists in this json.");

        var type = typeValue.ToString();
        var json = document.RootElement.GetRawText();

        return type switch
        {
            nameof(CommentAddedEvent) => JsonSerializer.Deserialize<CommentAddedEvent>(json, options),
            nameof(CommentDeletedEvent) => JsonSerializer.Deserialize<CommentDeletedEvent>(json, options),
            nameof(CommentUpdatedEvent) => JsonSerializer.Deserialize<CommentUpdatedEvent>(json, options),
            nameof(PostCreatedEvent) => JsonSerializer.Deserialize<PostCreatedEvent>(json, options),
            nameof(PostDeletedEvent) => JsonSerializer.Deserialize<PostDeletedEvent>(json, options),
            nameof(PostLikedEvent) => JsonSerializer.Deserialize<PostLikedEvent>(json, options),
            nameof(PostUpdatedEvent) => JsonSerializer.Deserialize<PostUpdatedEvent>(json, options),
            _ => throw new JsonException($"{type} is not supported yet!")
        };
    }

    public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
