using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Consumer.VisitsManagement.CancelVisit;

public sealed record CancelVisitCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}