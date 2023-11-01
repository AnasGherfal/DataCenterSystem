using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Management.RepresentativeManagement.ApproveRepresentative;

public sealed record ApproveRepresentativeCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}