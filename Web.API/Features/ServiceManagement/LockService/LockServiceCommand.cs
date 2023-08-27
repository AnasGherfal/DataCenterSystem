using MediatR;
using Shared.Dtos;

namespace Web.API.Features.ServiceManagement.LockService;

public sealed record LockServiceCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}