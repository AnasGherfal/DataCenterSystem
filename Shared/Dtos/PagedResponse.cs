namespace Shared.Dtos;

public class PagedResponse<T> : MessageResponse
{
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public List<T> Content { get; private set; }

    public PagedResponse(string message, IEnumerable<T>? items, int count, int pageNumber, int pageSize)
    {
        Msg = message;
        Content = new List<T>(items ?? Array.Empty<T>());
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int) Math.Ceiling(count / (double) pageSize);
    }
}