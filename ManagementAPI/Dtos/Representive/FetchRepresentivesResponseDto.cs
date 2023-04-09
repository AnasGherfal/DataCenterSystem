using ManagementAPI.Dtos.Customer;

namespace ManagementAPI.Dtos.Representive;

public class FetchVisitResponseDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public IList<VisitResponseDto> Content { get; set; }
}
