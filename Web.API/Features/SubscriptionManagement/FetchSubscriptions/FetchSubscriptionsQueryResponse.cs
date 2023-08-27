using Infrastructure.Constants;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptions
{
    public sealed record FetchSubscriptionsQueryResponse
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public GeneralStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
