using Infrastructure.Constants;

namespace Infrastructure.Models
{
    public class Representative : BaseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string IdentityNo { get; set; } = string.Empty;
        public short IdentityType { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public GeneralStatus Status { get; set; }
        public int CustomerId { get; set; }

        //--------relation
        public ICollection<RepresentativeFile> Files { get; set; } = new List<RepresentativeFile>();
        public Customer Customer { get; set; } = default!;
        public ICollection<RepresentativeVisit> RepresentativeVisits { get; set; } = new List<RepresentativeVisit>();
    }
}
