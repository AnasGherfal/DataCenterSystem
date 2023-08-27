using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.TimeShift;

public class TimeShiftCreatedEvent: EventStore<TimeShiftCreatedEventData>
{
    protected TimeShiftCreatedEvent() { }

    public TimeShiftCreatedEvent(Guid userId, Guid aggregateId, TimeShiftCreatedEventData data) : base(userId, aggregateId, 1, data)
    {
    }
}

public class TimeShiftCreatedEventData : IEventData
{
    public DateTime? Date { get; set; }
    public DayOfWeek? Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public decimal PriceForFirstHour { get; set; }
    public decimal PriceForRemainingHours { get; set; }
    [JsonIgnore]
    public EventType Type => EventType.TimeShiftCreated;
}