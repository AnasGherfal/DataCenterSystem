namespace ManagementAPI.Dtos.Subscriptions;

public class FetchSubscriptionFileResponseDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public IList<SubscriptionFileResponsDto>? Content { get; set; }
}
