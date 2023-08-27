using Core.Dtos;
using MediatR;

namespace Web.API.Features.VisitsManagement.EndVisit;

public sealed record EndVisitCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; } = string.Empty;
    public DateTime? EndTime { get; set; }

    public void SetId(string id)
    {
        Id = id;
    }
}