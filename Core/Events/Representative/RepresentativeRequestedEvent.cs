using System.Text.Json.Serialization;
using Core.Constants;
using Core.Events.Abstracts;

namespace Core.Events.Representative;

public class RepresentativeRequestedEvent : EventStore<RepresentativeRequestedEventData>
{
    protected RepresentativeRequestedEvent() { }

    public RepresentativeRequestedEvent(Guid userId, Guid aggregateId, RepresentativeRequestedEventData data) : base(userId, aggregateId, 1, data)
    {
    }
}

public class RepresentativeRequestedEventData : IEventData
{
    public Guid CustomerId { get; set; } = Guid.Empty;
    public RepresentativeType RepresentativeType { get; set; }
    public DateTime? ActiveFrom { get; set; }
    public DateTime? ActiveTo { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdentityNo { get; set; } = string.Empty;
    public IdentityType IdentityType { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public List<FileStorageData> Documents { get; set; } = new();
    [JsonIgnore]
    public EventType Type => EventType.RepresentativeRequested;
}