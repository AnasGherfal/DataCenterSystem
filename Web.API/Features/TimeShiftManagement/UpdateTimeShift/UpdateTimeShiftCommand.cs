using Core.Wrappers;
using MediatR;

namespace Web.API.Features.TimeShiftManagement.UpdateTimeShift;

public sealed record UpdateTimeShiftCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; } = string.Empty;
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public decimal? PriceForFirstHour { get; set; }
    public decimal? PriceForRemainingHours { get; set; }
    
    public void SetId(string id)
    {
        Id = id;
    }
}