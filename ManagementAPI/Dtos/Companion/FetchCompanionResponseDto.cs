using ManagementAPI.Dtos.Representive;

namespace ManagementAPI.Dtos.Companion;

public class FetchCompanionResponseDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public IList<CompanionResponseDto> Content { get; set; }
}
