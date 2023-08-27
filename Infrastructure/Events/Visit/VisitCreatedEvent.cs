using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Visit;

public class VisitCreatedEvent: EventStore<VisitCreatedEventData>
{
    protected VisitCreatedEvent() { }

    public VisitCreatedEvent(Guid userId, Guid aggregateId, VisitCreatedEventData data) : base(userId, aggregateId, 1, data)
    {
    }
}

public class VisitCreatedEventData : IEventData
{
    public Guid SubscriptionId { get; set; } = Guid.Empty;
    public Guid CustomerId { get; set; } = Guid.Empty;
    public VisitType VisitType { get; set; }
    public DateTime ExpectedStartTime { get; set; }
    public DateTime ExpectedEndTime { get; set; }
    public string Notes { get; set; } = string.Empty;
    public IList<Guid> Representatives { get; set; } = new List<Guid>();
    public IList<VisitCreatedEventDataExtra> Companions { get; set; } = new List<VisitCreatedEventDataExtra>();
    [JsonIgnore]
    public EventType Type => EventType.VisitCreated;
}


public sealed record VisitCreatedEventDataExtra
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdentityNo { get; set; } = string.Empty;
    public IdentityType IdentityType { get; set; }
    public string JobTitle { get; set; } = string.Empty;
}