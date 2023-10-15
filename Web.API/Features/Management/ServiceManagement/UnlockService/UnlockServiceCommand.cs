using Core.Wrappers;
using MediatR;

namespace Web.API.Features.ServiceManagement.UnlockService;

public sealed record UnlockServiceCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}