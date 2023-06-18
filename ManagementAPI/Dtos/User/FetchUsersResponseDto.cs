using ManagementAPI.Dtos.Customer;

namespace ManagementAPI.Dtos.User
{
    public class FetchUsersResponseDto
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public IList<UserResponseDto>? Content { get; set; }
    }
}
