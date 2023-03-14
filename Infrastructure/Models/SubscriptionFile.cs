namespace Infrastructure.Models;

public class SubscriptionFile : BaseModel
{
    public Guid Id { get; set; }
    //public short Type { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public byte[] Data { get; set; } = default!;
    public int SubscriptionId { get; set; }

    //--------------Relations

    public Subscription Subscription { get; set; } = new Subscription();


}
