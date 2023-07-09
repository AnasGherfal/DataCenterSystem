using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Admin;

public class AdminLockedAudit: AuditStore<AdminLockedAuditData>
{
    protected AdminLockedAudit() { }

    public AdminLockedAudit(string userId, Guid aggregateId, AdminLockedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class AdminLockedAuditData : IAuditData
{
    [JsonIgnore]
    public AuditType Type => AuditType.AdminLocked;
}