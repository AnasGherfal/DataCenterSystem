using Core.Dtos;
using MediatR;

namespace Web.API.Features.RepresentativeManagement.LockRepresentative;

public sealed record LockRepresentativeCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}