using Common.Constants;
using Infrastructure.Audits.Abstracts;
using Infrastructure.Audits.Admin;
using Infrastructure.Constants;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Audits.Customer;

public class CustomerCreatedAudit : AuditStore<CustomerCreatedAuditData>
{
    protected CustomerCreatedAudit() { }

    public CustomerCreatedAudit(string userId, Guid aggregateId, CustomerCreatedAuditData data) : base(userId, aggregateId, data)
    {
    }
}
public class CustomerCreatedAuditData : IAuditData
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string PrimaryPhone { get; set; } = string.Empty;
    public string? SecondaryPhone { get; set; } 
    public string Email { get; set; } = string.Empty;
    //TODO: Missing IList<FileRequestDto> Files prop.
    public IList<FileRequestDto> Files { get; set; }= new List<FileRequestDto>();
    [JsonIgnore]
    public AuditType Type => AuditType.CustomerCreated;
}