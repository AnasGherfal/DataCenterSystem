using Infrastructure.Constants;

namespace Infrastructure.Audits.Abstracts;

public abstract class Audit
{
    public long Id { get; set; }
    public Guid AggregateId { get; set; } = Guid.Empty;
    public AuditType Type { get; set; } = AuditType.None;
    public int Version { get; set; } = 1;
    public DateTime DateTime { get; set; } = DateTime.UtcNow;
    public string UserId { get; set; } = string.Empty;
}