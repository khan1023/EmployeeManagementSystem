using EMS.Application.User.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Application.Validators
{
    public class UpdateUserValidator : AbstractValidator<EditUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

            RuleFor(request => request.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(request => request.EmployeeType)
                .NotEmpty().WithMessage("Employee Type is required.");

            RuleFor(request => request.Designation)
                .NotEmpty().WithMessage("Designation is required.");

            RuleFor(request => request.LocationId)
                .NotEmpty().WithMessage("Location is required.");

            RuleFor(request => request.Nationality)
                .NotEmpty().WithMessage("Nationality is required.");

        }
    }
}
