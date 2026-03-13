using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.User;

public class ChangePasswordCommand:IRequest<Result>
{
    public string UserId { get; set; }=string.Empty;
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; }=string.Empty;
}
