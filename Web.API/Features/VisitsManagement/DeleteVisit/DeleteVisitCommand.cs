using Core.Wrappers;
using MediatR;

namespace Web.API.Features.VisitsManagement.DeleteVisit;

public sealed record DeleteVisitCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}