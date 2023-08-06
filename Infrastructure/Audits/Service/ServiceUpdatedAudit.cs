using Common.Constants;
using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Service;

public class ServiceUpdatedAudit: AuditStore<ServiceUpdatedAuditData>
{
    protected ServiceUpdatedAudit() { }

    public ServiceUpdatedAudit(string userId, Guid aggregateId, ServiceUpdatedAuditData data) : base(userId, aggregateId, data)
    {
    }
}

public class ServiceUpdatedAuditData : IAuditData
{
    public string Name { get; set; } = string.Empty;
    public string AmountOfPower { get; set; } = string.Empty;
    public string AcpPort { get; set; } = string.Empty;
    public string Dns { get; set; } = string.Empty;
    public int MonthlyVisits { get; set; } = 0;
    public decimal Price { get; set; } = 0;
    [JsonIgnore]
    public AuditType Type => AuditType.ServiceUpdated;
}