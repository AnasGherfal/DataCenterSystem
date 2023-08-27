using Infrastructure.Constants;
using MediatR;
using Shared.Dtos;

namespace Web.API.Features.RepresentativeManagement.UpdateRepresentativeFile;

public sealed record UpdateRepresentativeFileCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; } = string.Empty;
    public DocumentType? DocType { get; private set; }
    public IFormFile? File { get; private set; }
    
    public void SetId(string id)
    {
        Id = id;
    }
}