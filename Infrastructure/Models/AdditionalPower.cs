namespace Infrastructure.Models;

public class AdditionalPower : BaseModel
{
    public int Id { get; set; }
    public string Power { get; set; }= string.Empty;
    public decimal Price { get; set; }

    //-------Realations
   public ICollection<Subscription> Subscriptions { get; set; }=new List<Subscription>();
}
