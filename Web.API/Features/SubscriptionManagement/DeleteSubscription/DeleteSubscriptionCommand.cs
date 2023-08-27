using MediatR;
using Shared.Dtos;

namespace Web.API.Features.SubscriptionManagement.DeleteSubscription;

public sealed record DeleteSubscriptionCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}