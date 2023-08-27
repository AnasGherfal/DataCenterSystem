using Core.Dtos;
using MediatR;

namespace Web.API.Features.TimeShiftManagement.CreateTimeShift;

public sealed record CreateTimeShiftCommand: IRequest<MessageResponse>
{
    public DayOfWeek? Day { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public DateTime? Date { get; set; }
    public decimal? PriceForFirstHour { get; set; }
    public decimal? PriceForRemainingHours { get; set; }
}