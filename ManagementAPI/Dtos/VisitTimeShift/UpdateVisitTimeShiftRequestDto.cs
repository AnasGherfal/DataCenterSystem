namespace ManagementAPI.Dtos.VisitTimeShift;

public class UpdateVisitTimeShiftRequestDto
{
    public string Name { get; set; } = string.Empty;
    public decimal PriceForFirstHour { get; set; }
    public decimal PriceForRemainingHour { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
