using Core.Constants;
using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Consumer.SubscriptionsManagement.FetchMySubscriptions;
public sealed record FetchMySubscriptionsQuery : IRequest<PagedResponse<FetchMySubscriptionsQueryResponse>>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public SubscriptionStatus? Status { get; set; }
}