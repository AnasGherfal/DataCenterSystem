using Core.Constants;

namespace Web.API.Features.Consumer.SubscriptionsManagement.FetchMySubscriptions
{
    public sealed record FetchMySubscriptionsQueryResponse
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public GeneralStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
