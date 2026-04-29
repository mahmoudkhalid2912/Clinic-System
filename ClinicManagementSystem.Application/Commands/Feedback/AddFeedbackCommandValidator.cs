using ClinicManagementSystem.Application.Commands.Feedback;
using FluentValidation;
using System.Text.RegularExpressions;

public class AddFeedbackCommandValidator : AbstractValidator<AddFeedbackCommand>
{
    public AddFeedbackCommandValidator()
    {
        RuleFor(x => x.PatientName)
            .NotEmpty().WithMessage("Patient name is required.")
            .MinimumLength(3).WithMessage("Patient name must be at least 3 characters.")
            .MaximumLength(200).WithMessage("Patient name must not exceed 200 characters.");

        RuleFor(x => x.PatientPhone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Must(BeValidEgyptPhone)
            .WithMessage("Phone number must be a valid Egyptian number (e.g. 05XXXXXXXX or +2010XXXXXXXX).");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be between 1 and 5.");

        RuleFor(x => x.Comment)
            .MaximumLength(1000)
            .WithMessage("Notes must not exceed 1000 characters.");
    }

    private bool BeValidEgyptPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return false;

        // Accept both local (05XXXXXXXX) and international (+2010XXXXXXXX)
        return Regex.IsMatch(phone, @"^(\+20|0)(10|11|12|15)\d{8}$");
    }
}
