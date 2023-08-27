using Core.Dtos;
using MediatR;

namespace Web.API.Features.ServiceManagement.UnlockService;

public sealed record UnlockServiceCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}