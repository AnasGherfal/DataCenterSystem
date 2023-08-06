using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Service;

public class ServiceLockedAudit: AuditStore<ServiceLockedAuditData>
{
    protected ServiceLockedAudit() { }

    public ServiceLockedAudit(string userId, Guid aggregateId, ServiceLockedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class ServiceLockedAuditData : IAuditData
{
    [JsonIgnore]
    public AuditType Type => AuditType.ServiceLocked;
}