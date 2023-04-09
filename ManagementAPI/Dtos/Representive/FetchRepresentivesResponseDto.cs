
namespace ManagementAPI.Dtos.Representive;

public class FetchRepresentiveResponseDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public IList<RepresentiveResponseDto> Content { get; set; }
}
