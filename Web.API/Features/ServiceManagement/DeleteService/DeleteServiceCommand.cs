using Core.Wrappers;
using MediatR;

namespace Web.API.Features.ServiceManagement.DeleteService;

public sealed record DeleteServiceCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}