using FluentValidation;

namespace ManagementAPI.Dtos.User
{
    public class FetchUsersRequestDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
    public class FetchUsersRequestDtoFetchUsersRequestDtoalidator:AbstractValidator<FetchUsersRequestDto>
    {
        public FetchUsersRequestDtoFetchUsersRequestDtoalidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(a => a.PageNumber).GreaterThan(0).WithMessage("enter number graeter 0");
            RuleFor(a => a.PageSize).GreaterThan(0).LessThan(51).WithMessage("number of page size must be between 1 to 50");
        }
    }
}
