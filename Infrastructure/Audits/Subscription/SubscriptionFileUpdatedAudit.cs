using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Subscription;

public class SubscriptionFileUpdatedAudit: AuditStore<SubscriptionFileUpdatedAuditData>
{
    protected SubscriptionFileUpdatedAudit() { }

    public SubscriptionFileUpdatedAudit(string userId, Guid aggregateId, SubscriptionFileUpdatedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class SubscriptionFileUpdatedAuditData : IAuditData
{
    public IdentityType FileType { get; set; }
    public Guid FileIdentifier { get; set; }
    public string File { get; set; }
    [JsonIgnore]
    public AuditType Type => AuditType.SubscriptionFileUpdated;
}