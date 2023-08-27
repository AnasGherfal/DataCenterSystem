using MediatR;
using Shared.Dtos;

namespace Web.API.Features.SubscriptionManagement.RenewSubscription;

public sealed record RenewSubscriptionCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; } = string.Empty;
}