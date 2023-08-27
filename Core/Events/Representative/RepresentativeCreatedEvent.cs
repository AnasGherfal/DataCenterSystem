using System.Text.Json.Serialization;
using Core.Constants;
using Core.Events.Abstracts;

namespace Core.Events.Representative;

public class RepresentativeCreatedEvent : EventStore<RepresentativeCreatedEventData>
{
    protected RepresentativeCreatedEvent() { }

    public RepresentativeCreatedEvent(Guid userId, Guid aggregateId, RepresentativeCreatedEventData data) : base(userId, aggregateId, 1, data)
    {
    }
}

public class RepresentativeCreatedEventData : IEventData
{
    public Guid CustomerId { get; set; } = Guid.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdentityNo { get; set; } = string.Empty;
    public IdentityType IdentityType { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public List<FileStorageData> Documents { get; set; } = new();
    [JsonIgnore]
    public EventType Type => EventType.RepresentativeCreated;
}