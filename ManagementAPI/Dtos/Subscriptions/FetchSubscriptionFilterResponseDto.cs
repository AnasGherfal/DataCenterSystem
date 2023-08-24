namespace ManagementAPI.Dtos.Subscriptions;

public class FetchSubscriptionFilterResponseDto
{
    public IList<FilterSubscriptionResponseDto> FilteredContent { get; set; } = default!;
}
