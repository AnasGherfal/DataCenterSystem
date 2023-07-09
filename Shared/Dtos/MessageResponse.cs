using System.Text.Json.Serialization;

namespace Shared.Dtos;

public class MessageResponse
{
    [JsonPropertyName("msg")] 
    public string Msg { get; set; } = string.Empty;
}