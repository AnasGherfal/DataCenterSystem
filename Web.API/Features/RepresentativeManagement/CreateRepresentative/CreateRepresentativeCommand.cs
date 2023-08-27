using Core.Constants;
using Core.Wrappers;
using MediatR;

namespace Web.API.Features.RepresentativeManagement.CreateRepresentative;

public sealed record CreateRepresentativeCommand : IRequest<MessageResponse>
{
    public string? CustomerId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? IdentityNo { get; set; }
    public IdentityType? IdentityType { get; set; }
    public string? Email { get; set; }
    public string? PhoneNo { get; set; }
    public IList<CreateRepresentativeCommandItem>? Documents { get; set; }
}

public sealed record CreateRepresentativeCommandItem
{
    public IFormFile? File { get; private set; }
    public DocumentType? DocType { get; private set; }
}
