using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Service;

public class ServiceUnlockedAudit: AuditStore<ServiceUnlockedAuditData>
{
    protected ServiceUnlockedAudit() { }

    public ServiceUnlockedAudit(string userId, Guid aggregateId, ServiceUnlockedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class ServiceUnlockedAuditData : IAuditData
{
    [JsonIgnore]
    public AuditType Type => AuditType.ServiceUnlocked;
}