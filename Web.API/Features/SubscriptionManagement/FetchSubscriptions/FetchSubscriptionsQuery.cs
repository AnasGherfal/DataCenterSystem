using MediatR;
using Shared.Dtos;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptions;
public sealed record FetchSubscriptionsQuery : IRequest<PagedResponse<FetchSubscriptionsQueryResponse>>
{
    public string? CustomerId { get; set; } = string.Empty;
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}