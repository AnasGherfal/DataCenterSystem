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
    public IFormFile? IdentityDocument { get; private set; }
    public IFormFile? RepresentationDocument { get; private set; }
}
