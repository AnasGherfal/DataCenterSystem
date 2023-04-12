using ManagementAPI.Dtos.Visit;

namespace ManagementAPI.Dtos.VisitTimeShift;

public class FetchVisitTimeShiftResponseDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public IList<VisitTimeShiftResponseDto> Content { get; set; }
}
