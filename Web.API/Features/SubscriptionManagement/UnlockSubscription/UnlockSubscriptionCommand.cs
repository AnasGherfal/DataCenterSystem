using MediatR;
using Shared.Dtos;

namespace Web.API.Features.SubscriptionManagement.UnlockSubscription;

public sealed record UnlockSubscriptionCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; } = string.Empty;
}