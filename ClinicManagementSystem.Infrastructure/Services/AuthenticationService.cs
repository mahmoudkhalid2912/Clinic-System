using ClinicManagementSystem.Application.Abstractions.Authentication;
using ClinicManagementSystem.Application.Dtos.Authentication;
using ClinicManagementSystem.Domain.Abstractions;
using ClinicManagementSystem.Domain.Errors;
using ClinicManagementSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace ClinicManagementSystem.Infrastructure.Services;

public class AuthenticationService(UserManager<ApplicationUser> userManager,IJwtProvider jwtProvider) : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    private readonly int _refreshTokenExpiryInDays = 7; // Example refresh token expiry time

    public async Task<Result<LoginResponseDto?>> LoginAsync(string email, string password,CancellationToken cancellationToken=default)
    {
        //check if user exists
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return Result.Failure<LoginResponseDto?>(AuthErrors.UserNotFound);

        //check if password is correct
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

        if (!isPasswordValid)
            return Result.Failure<LoginResponseDto?>(AuthErrors.InvalidCredentials);

        // manual mapping to ApplicationUserDto for token generation
        ApplicationUserDto applicationUserDto = new ApplicationUserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email
        };


        // generate JWT token
        var (token, expiresIn) = _jwtProvider.GenerateToken(applicationUserDto);


        // generate refresh token
        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryInDays);
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
           ExpiresOn = refreshTokenExpiration
        });

        await _userManager.UpdateAsync(user);

        //return user info and token
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


    

    public async Task<Result<LoginResponseDto?>> GenerateRefreshTokenasync(string Token, string RefreshToken, CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.ValidateToken(Token);

        if (userId == null)
            return Result.Failure<LoginResponseDto?>(AuthErrors.InvalidToken);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Result.Failure<LoginResponseDto?>(AuthErrors.UserNotFound);

        var refreshTokenEntity =
            user.RefreshTokens.FirstOrDefault(rt => rt.Token == RefreshToken && rt.IsActive);

        if (refreshTokenEntity == null)
            return Result.Failure<LoginResponseDto?>(AuthErrors.RefreshTokenNotFound);

        refreshTokenEntity.RevokedOn = DateTime.UtcNow;

        //manual mapping to ApplicationUserDto for token generation

        ApplicationUserDto applicationUserDto = new ApplicationUserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email
        };

        // generate JWT token
        var (newtoken, expiresIn) = _jwtProvider.GenerateToken(applicationUserDto);


        // generate refresh token
        var newrefreshToken = GenerateRefreshToken();
        var newrefreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryInDays);
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newrefreshToken,
            ExpiresOn = newrefreshTokenExpiration
        });

        await _userManager.UpdateAsync(user);

        //return user info and token
        var response = new LoginResponseDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Token = newtoken,
            ExpiresIn = expiresIn,
            RefreshToken = newrefreshToken,
            RefreshTokenExpriation = newrefreshTokenExpiration
        };

        return Result.Success(response)!;
    }


  

    public async Task<Result<bool>> RevokeRefreshTokenasync(string Token, string RefreshToken, CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.ValidateToken(Token);

        if (userId == null)
            return Result.Failure<bool>(AuthErrors.InvalidToken);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Result.Failure<bool>(AuthErrors.UserNotFound);

        var refreshTokenEntity =
            user.RefreshTokens.FirstOrDefault(rt => rt.Token == RefreshToken && rt.IsActive);

        if (refreshTokenEntity == null)
            return Result.Failure<bool>(AuthErrors.RefreshTokenNotFound);

        refreshTokenEntity.RevokedOn = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);

        return Result.Success(true);
    }



    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    
}
