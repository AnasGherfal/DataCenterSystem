
namespace ManagementAPI.Dtos.Representative;

public class FetchRepresentativeResponseDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public IList<RepresentativeResponseDto>? Content { get; set; }
}
