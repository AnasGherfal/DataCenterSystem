﻿namespace Infrastructure.Models
{
    public class Representive : BaseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string IdentityNo { get; set; } = string.Empty;
        public short IdentityType { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public short Status { get; set; }
        public int CustomerId { get; set; }

        //--------relation
        public ICollection<RepresentiveFile> Files { get; set; } = new List<RepresentiveFile>();
        public Customer Customer { get; set; }=new Customer();
        public ICollection<RepresentiveVisit> RepresentiveVisits { get; set; } = new List<RepresentiveVisit>();




    }
}
