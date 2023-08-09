namespace Shared.Dtos;

public class ListResponse<T> : MessageResponse
{
    public List<T> Content { get; private set; }

    public ListResponse(string message, IEnumerable<T>? items)
    {
        Msg = message;
        Content = new List<T>(items ?? Array.Empty<T>());
    }
}