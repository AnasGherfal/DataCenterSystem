using Core.Wrappers;
using MediatR;

namespace Web.API.Features.SubscriptionManagement.RenewSubscription;

public sealed record RenewSubscriptionCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; } = string.Empty;
    public IFormFile? File { get; set; }
    
    public void SetId(string? value)
    {
        Id = value;
    }
}