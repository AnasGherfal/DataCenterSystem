﻿using FluentValidation;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;
using Common.Helpers;
using System.Text.RegularExpressions;
using System.Globalization;
using System;

namespace ManagementAPI.Dtos.Subscriptions;

public class FileDto
{
    public int ServiceId { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public IFormFile File { get; set; }

}

public class FileDtoValidator : AbstractValidator<FileDto>
{
    public FileDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(a => a.ServiceId)
           .NotEmpty().WithMessage("service id must be not empty")
           .GreaterThan(0).WithMessage("id must be greater than 0");
        RuleFor(a => a.CustomerId)
            .NotEmpty().WithMessage("customer id must be not empty")
            .GreaterThan(0).WithMessage("id must be greater than 0");
        //REVIEW: Variable is being treated like a string - instead confirm its a date first
        RuleFor(a => a.StartDate)
            .NotEmpty().WithMessage("startdate must be not empty");
        /*.Must(BeAValidDate).WithMessage("not valid datetime");*/
        //REVIEW: Variable is being treated like a string - instead confirm its a date first
         
        RuleFor(a => a.EndDate)
            .NotEmpty().WithMessage("end date must be not empty")
            .GreaterThan(a => a.StartDate).WithMessage("End date must be grater than start date")
            /*.Must(IsDateTimeValid)*/;
        
        When(p => IsPdf(p.File), () =>
        {
            RuleFor(p => p.File.Length).LessThanOrEqualTo(5000000);
        });


        RuleFor(a => a.File.Length).NotEmpty().LessThanOrEqualTo(1000000)
                            .WithMessage("File size is larger than allowed");
        RuleFor(a => a.File.ContentType).NotEmpty().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png") || x.Equals("application/pdf"));


    }
    public async Task<bool> IsDateTimeValid(string dateTimeString)
    {
        // Try to parse the date time string.
        DateTime dateTime;
        if (!DateTime.TryParseExact(dateTimeString, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
        {
            return false;
        }

        // The date time string is in the right format.
        return true;
    }
    private bool IsPdf(IFormFile file)
    {
        if (file.ContentType == "application/pdf")
            return true;
        else
            return false;
    }
}
/*public class CustomValidator : AbstractValidator<FileDto>
{
    public CustomValidator()
    {
        RuleFor(x => x.File).SetValidator(new FileDtoValidator());
    }
}*/