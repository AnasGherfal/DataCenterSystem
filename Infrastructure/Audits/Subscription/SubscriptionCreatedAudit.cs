using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Subscription;

public class SubscriptionCreatedAudit: AuditStore<SubscriptionCreatedAuditData>
{
    protected SubscriptionCreatedAudit() { }

    public SubscriptionCreatedAudit(string userId, Guid aggregateId, SubscriptionCreatedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class SubscriptionCreatedAuditData : IAuditData
{
    public Guid ServiceId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public IdentityType FileType { get; set; }
    public Guid FileIdentifier { get; set; }
    public string File { get; set; }
    [JsonIgnore]
    public AuditType Type => AuditType.SubscriptionCreated;
}