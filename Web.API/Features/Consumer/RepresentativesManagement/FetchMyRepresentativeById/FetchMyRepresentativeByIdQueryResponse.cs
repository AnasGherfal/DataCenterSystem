using Core.Constants;

namespace Web.API.Features.Consumer.RepresentativesManagement.FetchMyRepresentativeById
{
    public sealed record FetchMyRepresentativeByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = default!;
        public string IdentityNo { get; set; } = string.Empty;
        public IdentityType IdentityType { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public GeneralStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public IList<FetchMyRepresentativeByIdQueryResponseItem> Files { get; set; } = default!;
    }
    
    public sealed record FetchMyRepresentativeByIdQueryResponseItem
    {
        public Guid Id { get; set; }
        public DocumentType FileType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
