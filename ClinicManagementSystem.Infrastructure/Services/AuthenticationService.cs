using ClinicManagementSystem.Application.Abstractions.Authentication;
using ClinicManagementSystem.Application.Commands.Authentication.Register;
using ClinicManagementSystem.Application.Dtos.Authentication;
using ClinicManagementSystem.Domain.Abstractions;
using ClinicManagementSystem.Domain.Errors;
using ClinicManagementSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace ClinicManagementSystem.Infrastructure.Services;

public class AuthenticationService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IJwtProvider jwtProvider) : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    private readonly int _refreshTokenExpiryInDays = 25;
    private readonly int _maxRefreshTokens = 50;

    public async Task<Result<LoginResponseDto?>> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return Result.Failure<LoginResponseDto?>(UserError.UserNotFound);

        var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

        if (!signInResult.Succeeded)
        {
            return signInResult.IsLockedOut
                ? Result.Failure<LoginResponseDto?>(UserError.UserLocked)
                : Result.Failure<LoginResponseDto?>(AuthErrors.InvalidCredentials);
        }

        return await GenerateAuthResponseAsync(user);
    }

    public async Task<Result<LoginResponseDto?>> GenerateRefreshTokenasync(
        string token,
        string refreshToken,
        CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.ValidateToken(token);

        if (userId == null)
            return Result.Failure<LoginResponseDto?>(AuthErrors.InvaildToken);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Result.Failure<LoginResponseDto?>(UserError.UserNotFound);

        var refreshTokenEntity =
            user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken && rt.IsActive);

        if (refreshTokenEntity == null)
            return Result.Failure<LoginResponseDto?>(AuthErrors.RefreshTokenInvalid);

        refreshTokenEntity.RevokedOn = DateTime.UtcNow;

        return await GenerateAuthResponseAsync(user);
    }

    public async Task<Result<bool>> RevokeRefreshTokenasync(
        string token,
        string refreshToken,
        CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.ValidateToken(token);

        if (userId == null)
            return Result.Failure<bool>(AuthErrors.InvaildToken);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Result.Failure<bool>(UserError.UserNotFound);

        var refreshTokenEntity =
            user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken && rt.IsActive);

        if (refreshTokenEntity == null)
            return Result.Failure<bool>(AuthErrors.RefreshTokenInvalid);

        refreshTokenEntity.RevokedOn = DateTime.UtcNow;

        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            var error = updateResult.Errors.First();

            return Result.Failure<bool>(
                new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        return Result.Success(true);
    }

    public async Task<Result<LoginResponseDto?>> RegisterAsync(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser != null)
            return Result.Failure<LoginResponseDto?>(UserError.DublicatedEmail);

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            var error = result.Errors.First();

            return Result.Failure<LoginResponseDto?>(
                new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        return await GenerateAuthResponseAsync(user);
    }

    private async Task<Result<LoginResponseDto?>> GenerateAuthResponseAsync(ApplicationUser user)
    {
        var applicationUserDto = new ApplicationUserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email
        };

        var (token, expiresIn) = _jwtProvider.GenerateToken(applicationUserDto);

        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryInDays);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresOn = refreshTokenExpiration
        });

        // remove old tokens if more than 50
        if (user.RefreshTokens.Count > _maxRefreshTokens)
        {
            var tokensToRemove = user.RefreshTokens
                .OrderBy(rt => rt.CreatedOn)
                .Take(user.RefreshTokens.Count - _maxRefreshTokens)
                .ToList();

            foreach (var tokenToRemove in tokensToRemove)
            {
                user.RefreshTokens.Remove(tokenToRemove);
            }
        }

        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            var error = updateResult.Errors.First();

            return Result.Failure<LoginResponseDto?>(
                new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        var response = new LoginResponseDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Token = token,
            ExpiresIn = expiresIn,
            RefreshToken = refreshToken,
            RefreshTokenExpriation = refreshTokenExpiration
        };

        return Result.Success(response)!;
    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}