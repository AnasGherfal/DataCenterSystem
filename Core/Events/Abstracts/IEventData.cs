using Core.Constants;
using Newtonsoft.Json;

namespace Core.Events.Abstracts;

public interface IEventData
{
    [JsonIgnore]
    EventType Type { get; }
}