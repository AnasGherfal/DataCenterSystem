using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.TimeShift;

public class TimeShiftDeletedEvent: EventStore<TimeShiftDeletedEventData>
{
    protected TimeShiftDeletedEvent() { }

    public TimeShiftDeletedEvent(Guid userId, Guid aggregateId, long sequence, TimeShiftDeletedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class TimeShiftDeletedEventData : IEventData
{
    [JsonIgnore]
    public EventType Type => EventType.TimeShiftDeleted;
}