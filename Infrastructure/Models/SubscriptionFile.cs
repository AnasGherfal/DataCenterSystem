using Infrastructure.Constants;

namespace Infrastructure.Models;

public class SubscriptionFile : BaseModel
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public short IsActive { get; set; }
    public Guid SubscriptionId { get; set; }
    public Subscription Subscription { get; set; } = default!;
}
