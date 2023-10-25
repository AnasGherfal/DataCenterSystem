using Core.Wrappers;
using MediatR;

namespace Web.API.Features.VisitsManagement.StartVisit;

public sealed record StartVisitCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; } = string.Empty;
    public string? StartTime { get; set; }

    public void SetId(string id)
    {
        Id = id;
    }
}