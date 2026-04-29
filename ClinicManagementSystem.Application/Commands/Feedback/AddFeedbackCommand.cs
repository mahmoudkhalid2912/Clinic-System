using ClinicManagementSystem.Application.Dtos.Feedback;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Feedback;

public class AddFeedbackCommand : IRequest<Result<AddFeedbackResultDto>>
{
    public string PatientName { get; set; } = string.Empty;

    public string PatientPhone { get; set; } = string.Empty;

    public string Comment { get; set; } = string.Empty;

    public int Rating { get; set; }

    // Set internally from JWT - not sent by the client
    public string? PatientId { get; set; }
}
