using Infrastructure.Constants;
using MediatR;
using Shared.Dtos;

namespace Web.API.Features.CustomerManagement.UpdateCustomerFile;

public sealed record UpdateCustomerFileCommand : IRequest<MessageResponse>
{
    public string? CustomerId { get; private set; } = string.Empty;
    public string? FileId { get; private set; } = string.Empty;
    public DocumentType? DocType { get; private set; }
    public IFormFile? File { get; private set; }
    
    public void SetCustomerId(string id)
    {
        CustomerId = id;
    }
    
    public void SetFileId(string id)
    {
        FileId = id;
    }
}