using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Audits.Customer;

public class CustomerLockedAudit : AuditStore<CustomerLockedAuditData>
{
    protected CustomerLockedAudit() { }
    public CustomerLockedAudit(string userId, Guid aggregateId, CustomerLockedAuditData data) : base(userId, aggregateId, data)
    {
    }

}
public class CustomerLockedAuditData : IAuditData
{
    [JsonIgnore]
    public AuditType Type => AuditType.CustomerLocked;
}
