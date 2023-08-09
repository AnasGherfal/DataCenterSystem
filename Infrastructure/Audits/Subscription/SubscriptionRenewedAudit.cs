using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Subscription;

public class SubscriptionRenewedAudit: AuditStore<SubscriptionRenewedAuditData>
{
    protected SubscriptionRenewedAudit() { }

    public SubscriptionRenewedAudit(string userId, Guid aggregateId, SubscriptionRenewedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class SubscriptionRenewedAuditData : IAuditData
{
    [JsonIgnore]
    public AuditType Type => AuditType.SubscriptionRenewed;
}