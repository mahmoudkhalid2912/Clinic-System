using ClinicManagementSystem.Application.Dtos.Authentication;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Authentication.Login;

public class LoginCommand:IRequest<Result<LoginResponseDto?>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
