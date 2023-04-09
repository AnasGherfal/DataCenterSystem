using FluentValidation;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ManagementAPI.Dtos.Service
{
    public class CreateServiceDto
    {
        public string Name { get; set; } = string.Empty;
        public string AmountOfPower { get; set; } = string.Empty;
        public string AcpPort { get; set; } = string.Empty;
        public string Dns { get; set; } = string.Empty;
        public int MonthlyVisits { get; set; }
        public decimal Price { get; set; }
       
    }

    public class CreateServiceDtoValidator : AbstractValidator<CreateServiceDto>
    {
        public CreateServiceDtoValidator()
        {
            CascadeMode = CascadeMode.Stop;



            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("يرجى اختيار خدمة")
                .MinimumLength(3).WithMessage("name must be minimum lenght is 3 letters")
                .MaximumLength(100);


            RuleFor(x=>x.AmountOfPower)    
                .NotEmpty().WithMessage("Amount of power must be not null");


            RuleFor(x => x.AcpPort)
                .NotEmpty().WithMessage("AcpPort must be not null");



            RuleFor(x => x.Dns)
                .NotEmpty().WithMessage("Dnd must be not null");


            RuleFor(x => x.MonthlyVisits)
                .NotEmpty().WithMessage("monthly visit must be not null")
                .GreaterThan(0).WithMessage(" monthly visitis must be digit"); ;

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("price must be not null")
                .GreaterThan(0).WithMessage(" price must be digit");

                

        }

        
       
       


    }
}
