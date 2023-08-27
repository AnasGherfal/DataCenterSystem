namespace Infrastructure.Events.Abstracts;

public abstract class EventStore<T> : Event where T : IEventData
{
    public T Data { get; protected set; } = default!;

    protected EventStore() { }

    protected EventStore(Guid userId, Guid aggregateId, long sequence, T data, int version = 1)
    {
        AggregateId = aggregateId;
        Sequence = sequence;
        Type = data.Type;
        Data = data;
        DateTime = DateTime.UtcNow;
        Version = version;
        UserId = userId;
    }
}