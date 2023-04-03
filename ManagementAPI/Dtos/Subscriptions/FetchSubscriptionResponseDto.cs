using ManagementAPI.Dtos.Service;

namespace ManagementAPI.Dtos.Subscriptions
{
    public class FetchSubscriptionResponseDto
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public IList<SubscriptionResponseDto> Content { get; set; }
    }
}
