using ClinicManagementSystem.Application.Dtos.Authentication;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Authentication.ForgetPassword;

public class ForgetPasswordCommand:IRequest<Result<ForgetPasswordResponse>>
{
    public string Email { get; set; } = null!;
}
