using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Service;

public class ServiceCreatedAudit: AuditStore<ServiceCreatedAuditData>
{
    protected ServiceCreatedAudit() { }

    public ServiceCreatedAudit(string userId, Guid aggregateId, ServiceCreatedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class ServiceCreatedAuditData : IAuditData
{
    public string Name { get; set; } = string.Empty;
    public string AmountOfPower { get; set; } = string.Empty;
    public string AcpPort { get; set; } = string.Empty;
    public string Dns { get; set; } = string.Empty;
    public int MonthlyVisits { get; set; } = 0;
    public decimal Price { get; set; } = 0;
    public string Photo { get; set; } = string.Empty;
    [JsonIgnore]
    public AuditType Type => AuditType.ServiceCreated;
}