using Infrastructure.Audits.Abstracts;
using Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Audits.Customer;

public class CustomerUnlockedAudit : AuditStore<CustomerUnlockedAuditData>
{
    protected CustomerUnlockedAudit() { }
    public CustomerUnlockedAudit(string userId, Guid aggregateId, CustomerUnlockedAuditData data) : base(userId, aggregateId, data)
    {
    }

}
public class CustomerUnlockedAuditData : IAuditData
{
    [JsonIgnore]
    public AuditType Type => AuditType.CustomerUnlocked;
}
