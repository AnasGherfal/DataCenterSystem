using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Consumer.SubscriptionsManagement.FetchMySubscriptionById;

public sealed record FetchMySubscriptionByIdQuery: IRequest<ContentResponse<FetchMySubscriptionByIdQueryResponse>>
{
    public string? Id { get; set; }
}
