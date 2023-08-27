using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.TimeShift;

public class TimeShiftUpdatedEvent: EventStore<TimeShiftUpdatedEventData>
{
    protected TimeShiftUpdatedEvent() { }

    public TimeShiftUpdatedEvent(Guid userId, Guid aggregateId, long sequence, TimeShiftUpdatedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class TimeShiftUpdatedEventData : IEventData
{
    public decimal PriceForFirstHour { get; set; } = 0;
    public decimal PriceForRemainingHours { get; set; } = 0;
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    [JsonIgnore]
    public EventType Type => EventType.TimeShiftUpdated;
}