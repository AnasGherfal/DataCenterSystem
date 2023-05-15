using ManagementAPI.Dtos.Service;

namespace ManagementAPI.Dtos.Subscriptions
{
    public class FetchSubscriptionResponseDto
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public IList<SubscriptionRsponseDto> Content { get; set; }
    }
}
