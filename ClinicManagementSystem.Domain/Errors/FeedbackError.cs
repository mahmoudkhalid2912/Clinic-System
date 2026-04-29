using ClinicManagementSystem.Domain.Abstractions;
using System.Net;

namespace ClinicManagementSystem.Domain.Errors;

public static class FeedbackError
{
    public static Error InvalidRating =>
        new("Feedback.InvalidRating",
            "Rating must be between 1 and 5.",
            (int)HttpStatusCode.BadRequest);

    public static Error FeedbackCreationFailed =>
        new("Feedback.CreationFailed",
            "Failed to submit feedback. Please try again.",
            (int)HttpStatusCode.InternalServerError);
}
