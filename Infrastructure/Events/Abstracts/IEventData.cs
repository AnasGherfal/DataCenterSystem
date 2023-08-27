using Infrastructure.Constants;
using Newtonsoft.Json;

namespace Infrastructure.Events.Abstracts;

public interface IEventData
{
    [JsonIgnore]
    EventType Type { get; }
}