using ClinicManagementSystem.Application.Commands.User;
using ClinicManagementSystem.Application.Dtos.User;
using ClinicManagementSystem.Domain.Abstractions;

namespace ClinicManagementSystem.Application.Abstractions.User;

public interface IUserService
{
    Task<Result<UserProfileDto>> GetUserProfileAsync(string userId);

    Task<Result> ChangePasswordAsync(string UserId, ChangePasswordCommand request);
}
