using Infrastructure.Constants;
using MediatR;
using Shared.Dtos;

namespace Web.API.Features.SubscriptionManagement.UpdateSubscriptionFile;

public sealed record UpdateSubscriptionFileCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; } = string.Empty;
    public DocumentType? DocType { get; private set; }
    public IFormFile? File { get; private set; }
    
    public void SetId(string id)
    {
        Id = id;
    }
}