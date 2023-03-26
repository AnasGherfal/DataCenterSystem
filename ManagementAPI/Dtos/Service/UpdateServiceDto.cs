using FluentValidation;

namespace ManagementAPI.Dtos.Service;

public class UpdateServiceDto
{
    public string Name { get; set; } = string.Empty;
    public string AmountOfPower { get; set; } = string.Empty;
    public string AcpPort { get; set; } = string.Empty;
    public string Dns { get; set; } = string.Empty;
    public int MonthlyVisits { get; set; }
    public decimal Price { get; set; }


    public class UpdateServiceDtoValidator:AbstractValidator<UpdateServiceDto>
    {
        public UpdateServiceDtoValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(a => a.Name).NotEmpty().WithMessage("name must be not null");

            RuleFor(a => a.AmountOfPower).NotEmpty().WithMessage("AmountOfPower must be not null");

            RuleFor(a => a.AcpPort).NotEmpty().WithMessage("AcpPort must be not null");

            RuleFor(a => a.Dns).NotEmpty().WithMessage("Dns must be not null");

            RuleFor(a => a.MonthlyVisits)
                .NotEmpty().WithMessage("monthlyvisit must be not null")
                .GreaterThan(0).WithMessage("must be number more than 0");

            RuleFor(a => a.Price)
                .NotEmpty().WithMessage("price must be not null")
                .GreaterThan(0).WithMessage("must be number more than 0");
            


        }


    }
}
