using Core.Constants;
using Core.Wrappers;
using MediatR;

namespace Web.API.Features.RepresentativeManagement.UpdateRepresentative;

public sealed record UpdateRepresentativeCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; }
    public string? IdentityNo { get; set; }
    public IdentityType? IdentityType { get; set; } 
    public string? Email { get; set; }
    public string? PhoneNo { get; set; }
    public void SetId(string id) => Id = id;
}