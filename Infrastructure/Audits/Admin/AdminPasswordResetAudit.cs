using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Admin;

public class AdminPasswordResetAudit: AuditStore<AdminPasswordResetAuditData>
{
    protected AdminPasswordResetAudit() { }

    public AdminPasswordResetAudit(string userId, Guid aggregateId, AdminPasswordResetAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class AdminPasswordResetAuditData : IAuditData
{
    public string NewPassword { get; set; } = string.Empty;
    [JsonIgnore]
    public AuditType Type => AuditType.AdminPasswordReset;
}