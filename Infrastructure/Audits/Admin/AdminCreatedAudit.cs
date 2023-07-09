using Common.Constants;
using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Admin;

public class AdminCreatedAudit: AuditStore<AdminCreatedAuditData>
{
    protected AdminCreatedAudit() { }

    public AdminCreatedAudit(string userId, Guid aggregateId, AdminCreatedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class AdminCreatedAuditData : IAuditData
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public SystemPermissions Permissions { get; set; } = SystemPermissions.None;
    public int EmpId { get; set; } = 0;
    public string Password { get; set; } = string.Empty;
    [JsonIgnore]
    public AuditType Type => AuditType.AdminCreated;
}