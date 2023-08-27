using Infrastructure.Constants;
using Shared.Constants;

namespace Web.API.Features.RepresentativeManagement.FetchRepresentatives
{
    public sealed record FetchRepresentativesQueryResponse
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = default!;
        public string IdentityNo { get; set; } = string.Empty;
        public IdentityType IdentityType { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public GeneralStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
