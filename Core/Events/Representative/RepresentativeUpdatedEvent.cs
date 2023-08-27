using Core.Constants;
using Core.Events.Abstracts;
using Newtonsoft.Json;

namespace Core.Events.Representative;

public class RepresentativeUpdatedEvent: EventStore<RepresentativeUpdatedEventData>
{
    protected RepresentativeUpdatedEvent() { }

    public RepresentativeUpdatedEvent(Guid userId, Guid aggregateId, long sequence, RepresentativeUpdatedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class RepresentativeUpdatedEventData : IEventData
{
    public string IdentityNo { get; set; } = string.Empty;
    public IdentityType IdentityType { get; set; } 
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    [JsonIgnore]
    public EventType Type => EventType.RepresentativeUpdated;
}