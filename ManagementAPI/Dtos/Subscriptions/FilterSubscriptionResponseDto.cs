namespace ManagementAPI.Dtos.Subscriptions;

public class FilterSubscriptionResponseDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int Count { get; set; }
    public IList<SubscriptionRsponseDto> Content { get; set; } = new List<SubscriptionRsponseDto>();
}
