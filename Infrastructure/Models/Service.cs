namespace Infrastructure.Models
{
    public class Service 
        //: BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AmountOfPower { get; set; } = string.Empty;
        public string AcpPort { get; set; } = string.Empty;
        public string Dns { get; set; } = string.Empty;
        public int MonthlyVisits { get; set; }
        public decimal Price { get; set; }
        public short Status { get; set; }


        //------------Ralation

        public ICollection<Subscription> Subscriptions { get; set; }=new List<Subscription>();


    }
}
