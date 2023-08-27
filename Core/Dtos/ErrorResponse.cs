using System.Text.Json;

namespace Core.Dtos;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Msg { get; set; } = string.Empty;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
    }
}

