using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Management.SubscriptionManagement.RejectSubscription;

public sealed record RejectSubscriptionCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}