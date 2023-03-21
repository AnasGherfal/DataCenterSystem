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
        private readonly DataCenterContext _dbContext;

        public CreateServiceDtoValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("يرجى اختيار خدمة");


            RuleFor(x=>x.AmountOfPower)    
                .NotEmpty().WithMessage("Amount of power must be not null");


            RuleFor(x => x.AcpPort)
                .NotEmpty().WithMessage("AcpPort must be not null");



            RuleFor(x => x.Dns)
                .NotEmpty().WithMessage("Dnd must be not null");


            RuleFor(x => x.MonthlyVisits)
                .NotEmpty().WithMessage("monthly visit must be not null")
                .GreaterThan(0);

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("price must be not null")
                .GreaterThan(0);

                

        }

        
       
        private bool UniqueName(string name)
        {
            // ProjecteDataContext _db = new ProjecteDataContext();
            DataCenterContext dbContext = _dbContext;
            var category = dbContext.Services.Where(x => x.Name.ToLower() == name.ToLower()).SingleOrDefault();

            if (category == null) return true;
            return false;
        }


    }
}
