using ClinicManagementSystem.Application.Abstractions.User;
using ClinicManagementSystem.Application.Commands.User.ChangePassword;
using ClinicManagementSystem.Application.Dtos.User;
using ClinicManagementSystem.Domain.Abstractions;
using ClinicManagementSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Services;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

   

    public async Task<Result<UserProfileDto>> GetUserProfileAsync(string userId)
    {
        var User= await _userManager.Users.Where(x=>x.Id == userId)
        .SingleAsync();

        var UserProfile = new UserProfileDto
        {
            FullName = User.FullName,
            Email = User.Email!,
            PhoneNumber = User.PhoneNumber!
        };

        return Result.Success(UserProfile);
    }

    public async Task<Result> ChangePasswordAsync(string UserId, ChangePasswordCommand request)
    {
        var user = await _userManager.FindByIdAsync(UserId);

        var result = await _userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);

        if (result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();

        return Result.Failure(error: new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}
