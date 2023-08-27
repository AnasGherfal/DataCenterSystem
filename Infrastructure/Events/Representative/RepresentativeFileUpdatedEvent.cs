using Infrastructure.Constants;
using Infrastructure.Events.Abstracts;
using Newtonsoft.Json;

namespace Infrastructure.Events.Representative;

public class RepresentativeFileUpdatedEvent: EventStore<RepresentativeFileUpdatedEventData>
{
    protected RepresentativeFileUpdatedEvent() { }

    public RepresentativeFileUpdatedEvent(Guid userId, Guid aggregateId, long sequence, RepresentativeFileUpdatedEventData data) : base(userId, aggregateId, sequence, data)
    {
    }
}

public class RepresentativeFileUpdatedEventData : IEventData
{
    public Guid OldFileIdentifier { get; set; } = Guid.Empty;
    public Guid FileIdentifier { get; set; } = Guid.Empty;
    public DocumentType FileType { get; set; } = DocumentType.Passport;
    public string FileLink { get; set; } = string.Empty;
    [JsonIgnore]
    public EventType Type => EventType.RepresentativeFileUpdated;
}