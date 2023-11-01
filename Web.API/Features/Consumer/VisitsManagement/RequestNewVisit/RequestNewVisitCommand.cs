using Core.Constants;
using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Consumer.VisitsManagement.RequestNewVisit;

public sealed record RequestNewVisitCommand : IRequest<MessageResponse>
{
    public string? SubscriptionId { get; set; } = string.Empty;
    public VisitType? VisitType { get; set; }
    public DateTime? ExpectedStartTime { get; set; }
    public DateTime? ExpectedEndTime { get; set; }
    public string? Notes { get; set; }
    public IList<string> Representatives { get; set; } = new List<string>();
    public IList<RequestNewVisitCommandCompanion> Companions { get; set; } = new List<RequestNewVisitCommandCompanion>();
}

public sealed record RequestNewVisitCommandCompanion
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? IdentityNo { get; set; } = string.Empty;
    public IdentityType? IdentityType { get; set; }
    public string? JobTitle { get; set; } = string.Empty;
}