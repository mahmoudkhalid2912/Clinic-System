using ClinicManagementSystem.Domain.Abstractions;
using System.Net;

namespace ClinicManagementSystem.Domain.Errors;

public static class BookingError
{
    // =========================
    // ❌ Availability / Schedule
    // =========================

    public static Error NoScheduleFound =>
        new("Booking.NoScheduleFound",
            "No schedule found for the selected day.",
            (int)HttpStatusCode.NotFound);

    public static Error NoAvailableSlots =>
        new("Booking.NoAvailableSlots",
            "No available appointment slots for the selected date.",
            (int)HttpStatusCode.NotFound);

    public static Error ScheduleNotConfigured =>
        new("Booking.ScheduleNotConfigured",
            "Doctor schedule is not configured properly.",
            (int)HttpStatusCode.BadRequest);

    // =========================
    // ❌ Booking Conflicts
    // =========================

    public static Error SlotAlreadyBooked =>
        new("Booking.SlotAlreadyBooked",
            "This appointment slot is already booked.",
            (int)HttpStatusCode.Conflict);

    public static Error AppointmentTimeNotAvailable =>
        new("Booking.AppointmentTimeNotAvailable",
            "Selected appointment time is not available.",
            (int)HttpStatusCode.Conflict);

    public static Error BookingOverlap =>
        new("Booking.BookingOverlap",
            "This booking overlaps with another appointment.",
            (int)HttpStatusCode.Conflict);

    // =========================
    // ❌ Booking Validation
    // =========================

    public static Error InvalidAppointmentDate =>
        new("Booking.InvalidAppointmentDate",
            "Appointment date is invalid.",
            (int)HttpStatusCode.BadRequest);

    public static Error PastAppointmentNotAllowed =>
        new("Booking.PastAppointmentNotAllowed",
            "Cannot book an appointment in the past.",
            (int)HttpStatusCode.BadRequest);

    public static Error InvalidDuration =>
        new("Booking.InvalidDuration",
            "Appointment duration is invalid.",
            (int)HttpStatusCode.BadRequest);
    public static Error InvalidAppointmentTime=>
        new("Booking.InvalidAppointementTime",
            "Appointment Time is invalid.",
            (int)HttpStatusCode.BadRequest);
    // =========================
    // ❌ Booking State
    // =========================

    public static Error BookingNotFound =>
        new("Booking.BookingNotFound",
            "Booking not found.",
            (int)HttpStatusCode.NotFound);

    public static Error BookingAlreadyCancelled =>
        new("Booking.BookingAlreadyCancelled",
            "Booking is already cancelled.",
            (int)HttpStatusCode.Conflict);

    public static Error BookingCannotBeCancelled =>
        new("Booking.BookingCannotBeCancelled",
            "This booking cannot be cancelled.",
            (int)HttpStatusCode.BadRequest);

    public static Error BookingAlreadyCompleted =>
        new("Booking.BookingAlreadyCompleted",
            "Booking is already completed.",
            (int)HttpStatusCode.Conflict);

    // =========================
    // ❌ Patient / Access
    // =========================

    public static Error PatientNotAllowedToBook =>
        new("Booking.PatientNotAllowedToBook",
            "Patient is not allowed to book this appointment.",
            (int)HttpStatusCode.Forbidden);

    public static Error UnauthorizedBookingAccess =>
        new("Booking.UnauthorizedBookingAccess",
            "You are not allowed to access this booking.",
            (int)HttpStatusCode.Forbidden);

    // =========================
    // ❌ System Errors
    // =========================

    public static Error BookingCreationFailed =>
        new("Booking.BookingCreationFailed",
            "Failed to create booking.",
            (int)HttpStatusCode.BadRequest);

    public static Error BookingUpdateFailed =>
        new("Booking.BookingUpdateFailed",
            "Failed to update booking.",
            (int)HttpStatusCode.BadRequest);

    public static Error BookingDeletionFailed =>
        new("Booking.BookingDeletionFailed",
            "Failed to delete booking.",
            (int)HttpStatusCode.BadRequest);
}