using MediatR;
using Shared.Dtos;

namespace Web.API.Features.VisitsManagement.DeleteVisit;

public sealed record DeleteVisitCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}