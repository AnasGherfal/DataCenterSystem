using Infrastructure.Constants;

namespace Web.API.Features.CustomerManagement.FetchCustomers
{
    public sealed record FetchCustomersQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PrimaryPhone { get; set; } = string.Empty;
        public string SecondaryPhone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public GeneralStatus Status { get; set; }
        public int NumberOfFiles { get; set; } = 0;
        public int NumberOfSubscriptions { get; set; } = 0;
        public int NumberOfRepresentatives { get; set; } = 0;
        public DateTime CreatedOn { get; set; }
    }
}
