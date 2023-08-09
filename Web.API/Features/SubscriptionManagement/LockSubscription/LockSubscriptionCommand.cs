using MediatR;
using Shared.Dtos;

namespace Web.API.Features.SubscriptionManagement.LockSubscription;

public sealed record LockSubscriptionCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; } = string.Empty;
}