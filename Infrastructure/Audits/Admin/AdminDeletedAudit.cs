using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Admin;

public class AdminDeletedAudit: AuditStore<AdminDeletedAuditData>
{
    protected AdminDeletedAudit() { }

    public AdminDeletedAudit(string userId, Guid aggregateId, AdminDeletedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class AdminDeletedAuditData : IAuditData
{
    [JsonIgnore]
    public AuditType Type => AuditType.AdminDeleted;
}