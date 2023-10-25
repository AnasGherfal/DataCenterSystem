using Core.Wrappers;
using MediatR;

namespace Web.API.Features.RepresentativeManagement.UnlockRepresentative;

public sealed record UnlockRepresentativeCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}