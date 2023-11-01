using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Management.SubscriptionManagement.ApproveSubscription;

public sealed record ApproveSubscriptionCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}