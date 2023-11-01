using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Management.RepresentativeManagement.RejectRepresentative;

public sealed record RejectRepresentativeCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}