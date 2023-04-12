

namespace ManagementAPI.Dtos.Visit;

public class FetchVisitResponseDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public IList<VisitResponseDto> Content { get; set; }
}
