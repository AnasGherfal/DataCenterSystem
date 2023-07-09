using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Admin;

public class AdminUnlockedAudit: AuditStore<AdminUnlockedAuditData>
{
    protected AdminUnlockedAudit() { }

    public AdminUnlockedAudit(string userId, Guid aggregateId, AdminUnlockedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class AdminUnlockedAuditData : IAuditData
{
    [JsonIgnore]
    public AuditType Type => AuditType.AdminUnlocked;
}