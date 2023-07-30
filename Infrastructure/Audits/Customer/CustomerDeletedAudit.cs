using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Audits.Customer;

public class CustomerDeletedAudit : AuditStore<CustomerDeletedAuditData>
{
    protected CustomerDeletedAudit() { }
    public CustomerDeletedAudit(string userId, Guid aggregateId, CustomerDeletedAuditData data) : base(userId, aggregateId, data)
    {
    }
}
public class CustomerDeletedAuditData : IAuditData
{
    [JsonIgnore]
    public AuditType Type => AuditType.CustomerDeleted;
}