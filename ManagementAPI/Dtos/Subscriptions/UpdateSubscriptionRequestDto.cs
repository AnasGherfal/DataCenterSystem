using Infrastructure.Constants;

namespace ManagementAPI.Dtos.Subscriptions
{
    public class UpdateSubscriptionRequestDto
    {
        public int ServiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
