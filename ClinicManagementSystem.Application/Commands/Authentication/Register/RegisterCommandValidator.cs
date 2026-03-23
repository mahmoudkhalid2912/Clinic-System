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
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .Must(password => password.Any(char.IsUpper)).WithMessage("Password must contain at least one uppercase letter")
            .Must(password => password.Any(char.IsLower)).WithMessage("Password must contain at least one lowercase letter")
            .Must(password => password.Any(char.IsDigit)).WithMessage("Password must contain at least one number")
            .Must(password => password.Any(ch => "!@#$%^&*()_+/".Contains(ch))).WithMessage("Password must contain at least one special character from (!@#$%^&*()_+/)");

        // E.164 format: +[country code][subscriber number including area code]
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Invalid phone number format. +[country code][subscriber number including area code]");
    }
}
