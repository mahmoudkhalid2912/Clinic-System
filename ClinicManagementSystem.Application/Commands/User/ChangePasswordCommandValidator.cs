using FluentValidation;

namespace ClinicManagementSystem.Application.Commands.User;

public class ChangePasswordCommandValidator:AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(8)
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number")
            .Matches(@"[\!\?\*\.\@\#\$\%\^\&]").WithMessage("Password must contain at least one special character");

        RuleFor(x => x.NewPassword)
    .NotEqual(x => x.CurrentPassword)
    .WithMessage("New password must be different from current password");
    }
}
