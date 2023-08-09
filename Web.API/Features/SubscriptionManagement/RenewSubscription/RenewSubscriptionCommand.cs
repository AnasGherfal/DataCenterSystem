using MediatR;
using Shared.Dtos;

namespace Web.API.Features.SubscriptionManagement.RenewSubscription;

public sealed record RenewSubscriptionCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; } = string.Empty;
    
    public void SetId(string id)
    {
        Id = id;
    }
}