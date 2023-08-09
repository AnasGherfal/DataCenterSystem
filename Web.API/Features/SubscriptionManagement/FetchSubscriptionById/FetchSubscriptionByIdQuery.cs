using MediatR;
using Shared.Dtos;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptionById;
public sealed record FetchSubscriptionByIdQuery(string? Id) 
    : IRequest<ContentResponse<FetchSubscriptionByIdQueryResponse>>;
