using ClinicManagementSystem.Application.Commands.Booking;
using FluentValidation;
using System.Text.RegularExpressions;

public class AddBookingCommandValidator : AbstractValidator<AddBookingCommand>
{
    public AddBookingCommandValidator()
    {
        
        RuleFor(x => x.AppointmentDate)
            .NotEmpty()
            .Must(date => date.Date >= DateTime.UtcNow.Date)
            .WithMessage("Appointment date cannot be in the past.");

        
        RuleFor(x => x.AppointmentTime)
            .NotEmpty()
            .Must(time => time != default)
            .WithMessage("Appointment time is required.");

        
        RuleFor(x => x.PatientName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200)
            .WithMessage("Patient name must be between 3 and 200 characters.");

        RuleFor(x => x.PatientPhone)
            .NotEmpty()
            .Must(BeValidEgyptPhone)
            .WithMessage("Phone number must be a valid Egyptian number like +2010XXXXXXXX.");
    }

        private bool BeValidEgyptPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return false;

        return Regex.IsMatch(phone, @"^\+20(10|11|12|15)\d{8}$");
    }
}
