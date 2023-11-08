using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Management.SubscriptionManagement.ApproveSubscription;

public sealed record ApproveSubscriptionCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; }
    public string? ContractNumber { get; set; }
    public string? ContractDate { get; set; }
    public void SetId(string id) => Id = id;
}