using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Subscription;

public class SubscriptionUnlockedAudit: AuditStore<SubscriptionUnlockedAuditData>
{
    protected SubscriptionUnlockedAudit() { }

    public SubscriptionUnlockedAudit(string userId, Guid aggregateId, SubscriptionUnlockedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class SubscriptionUnlockedAuditData : IAuditData
{
    [JsonIgnore]
    public AuditType Type => AuditType.SubscriptionUnlocked;
}