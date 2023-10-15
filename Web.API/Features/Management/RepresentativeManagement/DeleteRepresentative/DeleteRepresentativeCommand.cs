using Core.Wrappers;
using MediatR;

namespace Web.API.Features.RepresentativeManagement.DeleteRepresentative;

public sealed record DeleteRepresentativeCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}