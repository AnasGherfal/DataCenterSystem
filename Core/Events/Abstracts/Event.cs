using Core.Constants;

namespace Core.Events.Abstracts;

public abstract class Event
{
    public long Id { get; set; }
    public Guid AggregateId { get; set; } = Guid.Empty;
    public long Sequence { get; set; } = 1;
    public EventType Type { get; set; }
    public int Version { get; set; } = 1;
    public DateTime DateTime { get; set; } = DateTime.UtcNow;
    public Guid UserId { get; set; } = Guid.Empty;
}