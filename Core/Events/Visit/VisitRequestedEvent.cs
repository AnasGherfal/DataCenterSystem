using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Visit;

public class VisitRequestedEvent: EventStore<VisitRequestedEventData>
{
    protected VisitRequestedEvent() { }

    public VisitRequestedEvent(Guid userId, Guid aggregateId, VisitRequestedEventData data) : base(userId, aggregateId, 1, data)
    {
    }
}

public class VisitRequestedEventData : IEventData
{
    public Guid SubscriptionId { get; set; } = Guid.Empty;
    public Guid CustomerId { get; set; } = Guid.Empty;
    public VisitType VisitType { get; set; }
    public DateTime ExpectedStartTime { get; set; }
    public DateTime ExpectedEndTime { get; set; }
    public string Notes { get; set; } = string.Empty;
    public IList<Guid> Representatives { get; set; } = new List<Guid>();
    public IList<VisitRequestedEventDataExtra> Companions { get; set; } = new List<VisitRequestedEventDataExtra>();
    [JsonIgnore]
    public EventType Type => EventType.VisitRequested;
}


public sealed record VisitRequestedEventDataExtra
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdentityNo { get; set; } = string.Empty;
    public IdentityType IdentityType { get; set; }
    public string JobTitle { get; set; } = string.Empty;
}