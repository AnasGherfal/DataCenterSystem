using Core.Dtos;
using MediatR;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptions;
public sealed record FetchSubscriptionsQuery : IRequest<PagedResponse<FetchSubscriptionsQueryResponse>>
{
    public string? CustomerId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}