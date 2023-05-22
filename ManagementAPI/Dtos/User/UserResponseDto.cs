using Infrastructure.Constants;

namespace ManagementAPI.Dtos.User
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public short Status { get; set; }
        public long Permisssions { get; set; } = 0;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int EmpId { get; set; }
    }
}
