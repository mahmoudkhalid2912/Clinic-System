using FluentValidation;

namespace ClinicManagementSystem.Application.Commands.Authentication.RefreshToken;

public class RefreshTokenCommandValidator: AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.");

        RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("Token is required.");
    }
}
