using Core.Dtos;
using MediatR;

namespace Web.API.Features.VisitsManagement.DeleteVisit;

public sealed record DeleteVisitCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}