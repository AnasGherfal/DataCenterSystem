using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Service;

public class ServiceDeletedAudit: AuditStore<ServiceDeletedAuditData>
{
    protected ServiceDeletedAudit() { }

    public ServiceDeletedAudit(string userId, Guid aggregateId, ServiceDeletedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class ServiceDeletedAuditData : IAuditData
{
    [JsonIgnore]
    public AuditType Type => AuditType.ServiceDeleted;
}