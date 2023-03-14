namespace Infrastructure.Models;

public class SubscriptionAdditionalPower : BaseModel
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public int AdditionalPowerId { get; set; }

    //---------Relations
    public Subscription Subscription { get; set; } = new Subscription();
    public AdditionalPower AdditionalPower { get; set; } = new AdditionalPower();
}
