﻿using Core.Validators;
using FluentValidation;

namespace Web.API.Features.CustomerManagement.UpdateCustomerFile;

public class UpdateCustomerFileCommandValidator: AbstractValidator<UpdateCustomerFileCommand>
{
    public UpdateCustomerFileCommandValidator()
    {
        When(p => !string.IsNullOrWhiteSpace(p.FileId), () =>
        {
            RuleFor(p => p.FileId)
                .IsGuid();
        });

        RuleFor(x => x.File)
            .SetValidator(new DocumentFileValidator("File is required."));
    }
}