namespace ManagementAPI.Dtos.Subscriptions
{
    public class SubscriptionRsponseDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public short Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
