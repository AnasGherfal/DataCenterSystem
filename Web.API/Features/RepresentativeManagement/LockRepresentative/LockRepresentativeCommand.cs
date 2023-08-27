using MediatR;
using Shared.Dtos;

namespace Web.API.Features.RepresentativeManagement.LockRepresentative;

public sealed record LockRepresentativeCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}