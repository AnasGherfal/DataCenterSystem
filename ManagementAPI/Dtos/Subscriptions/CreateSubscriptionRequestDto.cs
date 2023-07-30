using FluentValidation;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;
using Common.Helpers;
using System.Text.RegularExpressions;
using System.Globalization;
using System;
using Shared.Dtos;

namespace ManagementAPI.Dtos.Subscriptions;

public class CreateSubscriptionRequestDto
{
    public Guid ServiceId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public FileRequestDto File { get; set; } = default!;

}

public class CreateSubscriptionRequestDtoValidator : AbstractValidator<CreateSubscriptionRequestDto>
{
    [Obsolete("CreateSubscriptionRequestDto")]
    public CreateSubscriptionRequestDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(a => a.ServiceId)
           .NotEmpty().WithMessage("service id must be not empty");

        RuleFor(a => a.CustomerId)
            .NotEmpty().WithMessage("customer id must be not empty");
            
        //REVIEW: Variable is being treated like a string - instead confirm its a date first
        RuleFor(a => a.StartDate)
            .NotEmpty().WithMessage("startdate must be not empty");
        /*.Must(BeAValidDate).WithMessage("not valid datetime");*/
        //REVIEW: Variable is being treated like a string - instead confirm its a date first

        RuleFor(a => a.EndDate)
            .NotEmpty().WithMessage("end date must be not empty")
            .GreaterThan(a => a.StartDate).WithMessage("End date must be grater than start date");
            
        
        /*When(p => IsPdf(p.File.File), () =>
        {
            RuleFor(p => p.File.File.Length).LessThanOrEqualTo(5000000);
        });


        RuleFor(a => a.File.File.Length).NotEmpty().LessThanOrEqualTo(1000000)
                            .WithMessage("File size is larger than allowed");
        RuleFor(a => a.File.File.ContentType).NotEmpty().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png") || x.Equals("application/pdf"));
*/

    }
   /*private bool  IsDateTimeValid(string dateTimeString)
    {
        // Try to parse the date time string.
        DateTime dateTime;
         if (!DateTime.TryParseExact(dateTimeString, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
        {
             return false;
        }

        // The date time string is in the right format.
             return true;
    }*/
    private static bool IsPdf(FormFile file)
    {
        if (file.ContentType == "application/pdf")
            return true;
        else
            return false;
    }
}
