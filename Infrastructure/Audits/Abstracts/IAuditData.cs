using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Audits.Abstracts;

public interface IAuditData
{
    [JsonIgnore]
    AuditType Type { get; }
}