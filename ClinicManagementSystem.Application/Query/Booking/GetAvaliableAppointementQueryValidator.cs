using FluentValidation;

namespace ClinicManagementSystem.Application.Query.Booking;

public class GetAvaliableAppointementQueryValidator:AbstractValidator<GetAvailableAppointmentsQuery>
{
	public GetAvaliableAppointementQueryValidator()
	{
        RuleFor(x => x.Date)
            .Cascade(CascadeMode.Stop)

            // 1. لازم تكون موجودة
            .NotNull()
            .WithMessage("Date is required")

            // 2. مش أقل من النهارده
            .Must(date => date.Date >= DateTime.Today)
            .WithMessage("Date must be today or in the future")

            // 3. (اختياري) تمنع أي وقت غير 00:00:00
            .Must(date => date.TimeOfDay == TimeSpan.Zero)
            .WithMessage("Time is not allowed, only date");
    }
}
