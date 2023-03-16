﻿namespace LTTDataCenter.DTOs.Requests.Create
{
    public class CreateRepresentive
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string IdentityNo { get; set; } = string.Empty;
        public short IdentityType { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
    }
}
