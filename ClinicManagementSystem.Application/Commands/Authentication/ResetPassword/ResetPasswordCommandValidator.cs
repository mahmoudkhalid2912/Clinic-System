using FluentValidation;

namespace ClinicManagementSystem.Application.Commands.Authentication.ResetPassword;

public class ResetPasswordCommandValidator:AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x=> x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .Must(password => password.Any(char.IsUpper)).WithMessage("Password must contain at least one uppercase letter")
            .Must(password => password.Any(char.IsLower)).WithMessage("Password must contain at least one lowercase letter")
            .Must(password => password.Any(char.IsDigit)).WithMessage("Password must contain at least one number")
            .Must(password => password.Any(ch => "!@#$%^&*()_+/".Contains(ch)))
            .WithMessage("Password must contain at least one special character from (!@#$%^&*()_+/)");

        RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Token is required");
    }
}
