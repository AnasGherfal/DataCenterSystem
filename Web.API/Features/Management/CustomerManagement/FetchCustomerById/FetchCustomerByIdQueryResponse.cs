using Core.Constants;

namespace Web.API.Features.CustomerManagement.FetchCustomerById
{
    public sealed record FetchCustomerByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PrimaryPhone { get; set; } = string.Empty;
        public string SecondaryPhone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public GeneralStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public IList<FetchCustomerByIdQueryResponseItem> Files { get; set; } = default!;
    }
    
    public sealed record FetchCustomerByIdQueryResponseItem
    {
        public Guid Id { get; set; }
        public DocumentType FileType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
