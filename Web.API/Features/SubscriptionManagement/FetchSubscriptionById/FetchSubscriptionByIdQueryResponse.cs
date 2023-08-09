using Infrastructure.Constants;
using Shared.Constants;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptionById
{
    public class FetchSubscriptionByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public GeneralStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedOn { get; set; }
        public IList<FetchSubscriptionByIdQueryResponseItem> Files { get; set; } = default!;
    }
    
    public class FetchSubscriptionByIdQueryResponseItem
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public IdentityType FileType { get; set; }
        public GeneralStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
