using Core.Wrappers;
using MediatR;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptionById;

public sealed record FetchSubscriptionByIdQuery: IRequest<ContentResponse<FetchSubscriptionByIdQueryResponse>>
{
    public string? Id { get; set; }
}
