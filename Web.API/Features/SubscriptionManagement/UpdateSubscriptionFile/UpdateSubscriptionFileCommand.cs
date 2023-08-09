using MediatR;
using Shared.Constants;
using Shared.Dtos;

namespace Web.API.Features.SubscriptionManagement.UpdateSubscriptionFile;

public sealed record UpdateSubscriptionFileCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; } = string.Empty;
    public string? FileId { get; set; } = string.Empty;
    public IdentityType? DocType { get; private set; }
    public IFormFile? File { get; private set; }
}