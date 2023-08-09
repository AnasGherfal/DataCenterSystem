using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Subscription;

public class SubscriptionLockedAudit: AuditStore<SubscriptionLockedAuditData>
{
    protected SubscriptionLockedAudit() { }

    public SubscriptionLockedAudit(string userId, Guid aggregateId, SubscriptionLockedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class SubscriptionLockedAuditData : IAuditData
{
    [JsonIgnore]
    public AuditType Type => AuditType.SubscriptionLocked;
}