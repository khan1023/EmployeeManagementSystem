using EMS.Application.User.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Application.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
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

            RuleFor(request => request.PassportFile)
            .NotEmpty()
            .When(request => !string.IsNullOrEmpty(request.PassportNo) || !string.IsNullOrEmpty(request.PassportExpireDate))
            .WithMessage("Please Upload Passport File.");

            RuleFor(request => request.PassportNo)
           .NotEmpty()
           .When(request => request.PassportFile != null)
           .WithMessage("Please Enter Passport Number.");

            RuleFor(request => request.PassportExpireDate)
          .NotEmpty()
          .When(request => request.PassportFile != null)
          .WithMessage("Please Select Passport Expire Date.");


            RuleFor(request => request.PassportFile)
            .Must((request, file) =>
                file != null || request.EmirateIdFile != null || request.DrivingLicenceFile != null)
            .WithMessage("Please Select atleast one file form Passport,Driving Licence or Emirate Id File.");

            RuleFor(request => request.DrivingLicenceFile)
                .Must((request, file) =>
                    file != null || request.EmirateIdFile != null || request.PassportFile != null)
                .WithMessage("Please Select atleast one file form Passport,Driving Licence or Emirate Id File.");

            RuleFor(request => request.EmirateIdFile)
                .Must((request, file) =>
                    file != null || request.PassportFile != null || request.DrivingLicenceFile != null)
                .WithMessage("Please Select atleast one file form Passport,Driving Licence or Emirate Id File.");
        }
    }
}
