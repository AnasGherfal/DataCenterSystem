namespace Infrastructure.Audits.Abstracts;

public abstract class AuditStore<T> : Audit where T : IAuditData
{
    public T Data { get; protected set; } = default!;

    protected AuditStore() { }

    protected AuditStore(string userId, Guid aggregateId, T data, int version = 1)
    {
        AggregateId = aggregateId;
        Type = data.Type;
        Data = data;
        DateTime = DateTime.UtcNow;
        Version = version;
        UserId = userId;
    }
}