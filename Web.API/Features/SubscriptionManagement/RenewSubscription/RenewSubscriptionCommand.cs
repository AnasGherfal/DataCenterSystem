using Core.Dtos;
using MediatR;

namespace Web.API.Features.SubscriptionManagement.RenewSubscription;

public sealed record RenewSubscriptionCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; } = string.Empty;
}