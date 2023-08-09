using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Subscription;

public class SubscriptionDeletedAudit: AuditStore<SubscriptionDeletedAuditData>
{
    protected SubscriptionDeletedAudit() { }

    public SubscriptionDeletedAudit(string userId, Guid aggregateId, SubscriptionDeletedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class SubscriptionDeletedAuditData : IAuditData
{
    [JsonIgnore]
    public AuditType Type => AuditType.SubscriptionDeleted;
}