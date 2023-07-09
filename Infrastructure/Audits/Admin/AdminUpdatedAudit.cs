using Common.Constants;
using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Admin;

public class AdminUpdatedAudit: AuditStore<AdminUpdatedAuditData>
{
    protected AdminUpdatedAudit() { }

    public AdminUpdatedAudit(string userId, Guid aggregateId, AdminUpdatedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class AdminUpdatedAuditData : IAuditData
{
    public SystemPermissions? Permissions { get; set; } = SystemPermissions.None;
    [JsonIgnore]
    public AuditType Type => AuditType.AdminUpdated;
}