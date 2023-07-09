using System.Text.Json.Serialization;

namespace Shared.Dtos;

public class ContentResponse<T> : MessageResponse
{
    [JsonPropertyName("content")]
    public T Content { get; set; }
    
    public ContentResponse(string message, T content)
    {
        Msg = message;
        Content = content;
    }
}