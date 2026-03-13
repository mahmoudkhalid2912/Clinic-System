using FluentValidation;

namespace ClinicManagementSystem.Application.Commands.Authentication.Register;

public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full Name is required.")
            .MaximumLength(150).WithMessage("Full Name cannot exceed 150 characters.");

        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        // password at least 8 characters, at least one uppercase letter, one lowercase letter, one number and one special character
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage("password at least 8 characters, " +
            "at least one uppercase letter, one lowercase letter, one number and one special character");

        // E.164 format: +[country code][subscriber number including area code]
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Invalid phone number format. +[country code][subscriber number including area code]");
    }
}
