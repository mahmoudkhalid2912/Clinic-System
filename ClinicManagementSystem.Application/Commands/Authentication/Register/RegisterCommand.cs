using ClinicManagementSystem.Application.Dtos.Authentication;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.Authentication.Register;

public class RegisterCommand:IRequest<Result<LoginResponseDto?>>
{
    public string FullName { get; set; }=string.Empty;
    public string Email { get; set; }=string.Empty;

    public string Password { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}
