using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Audits.Customer;

public class CustomerUpdatedAudit : AuditStore<CustomerUpdatedAuditData>
{
    protected CustomerUpdatedAudit() { }
    public CustomerUpdatedAudit(string userId, Guid aggregateId, CustomerUpdatedAuditData data) : base(userId, aggregateId, data)
    {
    }

}
public class CustomerUpdatedAuditData : IAuditData
{
    
    public string Name { get; set; }=string.Empty;
    public string? Address { get; set; }
    public string PrimaryPhone { get; set; } = string.Empty;
    public string? SecondaryPhone { get; set; }
    public string Email { get; set; } =string.Empty;
    public IList<FileRequestDto> Files { get; set; }=new List<FileRequestDto>();
    [JsonIgnore]
    public AuditType Type => AuditType.CustomerUpdated;
}
