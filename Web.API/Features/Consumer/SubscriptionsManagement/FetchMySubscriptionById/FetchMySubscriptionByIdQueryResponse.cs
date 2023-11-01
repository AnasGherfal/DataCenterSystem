using Core.Constants;

namespace Web.API.Features.Consumer.SubscriptionsManagement.FetchMySubscriptionById
{
    public sealed record FetchMySubscriptionByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public GeneralStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedOn { get; set; }
        public IList<FetchMySubscriptionByIdQueryResponseItem> Files { get; set; } = default!;
    }
    
    public sealed record FetchMySubscriptionByIdQueryResponseItem
    {
        public Guid Id { get; set; }
        public DocumentType FileType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
