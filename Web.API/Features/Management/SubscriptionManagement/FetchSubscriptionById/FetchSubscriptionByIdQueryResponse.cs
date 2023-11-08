using Core.Constants;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptionById
{
    public sealed record FetchSubscriptionByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; } = default!;
        public string ContractNumber { get; set; } = default!;
        public DateTime ContractDate { get; set; }
        public string CustomerName { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public GeneralStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedOn { get; set; }
        public IList<FetchSubscriptionByIdQueryResponseItem> Files { get; set; } = default!;
    }
    
    public sealed record FetchSubscriptionByIdQueryResponseItem
    {
        public Guid Id { get; set; }
        public DocumentType FileType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
