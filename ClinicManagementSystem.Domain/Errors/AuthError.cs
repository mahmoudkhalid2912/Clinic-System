using ClinicManagementSystem.Domain.Abstractions;
using System.Net;

namespace ClinicManagementSystem.Domain.Errors;

public static class AuthErrors
{
    public static Error UserNotFound =>
        new("Auth.UserNotFound",
            "User with the provided email does not exist.",
            (int)HttpStatusCode.NotFound);

    public static Error InvalidCredentials =>
        new("Auth.InvalidCredentials",
            "Invalid email or password.",
            (int)HttpStatusCode.Unauthorized);

    public static Error InvalidToken =>
        new("Auth.InvalidToken",
            "The provided token is invalid.",
            (int)HttpStatusCode.Unauthorized);

    public static Error RefreshTokenNotFound =>
        new("Auth.RefreshTokenNotFound",
            "Refresh token does not exist.",
            (int)HttpStatusCode.NotFound);

    public static Error RefreshTokenExpired =>
        new("Auth.RefreshTokenExpired",
            "Refresh token has expired.",
            (int)HttpStatusCode.Unauthorized);

    public static Error RefreshTokenInactive =>
        new("Auth.RefreshTokenInactive",
            "Refresh token is no longer active.",
            (int)HttpStatusCode.Unauthorized);

    public static Error TokenValidationFailed =>
        new("Auth.TokenValidationFailed",
            "Token validation failed.",
            (int)HttpStatusCode.Unauthorized);
}