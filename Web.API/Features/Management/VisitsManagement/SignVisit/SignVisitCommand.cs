using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Management.VisitsManagement.SignVisit;

public sealed record SignVisitCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; } = string.Empty;
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }

    public void SetId(string id)
    {
        Id = id;
    }
}