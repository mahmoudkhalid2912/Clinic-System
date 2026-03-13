using ClinicManagementSystem.Domain.Abstractions;
using System.Net;

namespace ClinicManagementSystem.Domain.Errors;

public static class AuthErrors
{
    public static Error PasswordTooWeak =>
    new("Auth.PasswordTooWeak",
        "Password does not meet security requirements.",
        (int)HttpStatusCode.BadRequest);

    public static Error PasswordMismatch =>
        new("Auth.PasswordMismatch",
            "Passwords do not match.",
            (int)HttpStatusCode.BadRequest);

    public static Error OldPasswordIncorrect =>
        new("Auth.OldPasswordIncorrect",
            "Old password is incorrect.",
            (int)HttpStatusCode.Unauthorized);

    public static Error LoginFailed =>
        new("Auth.LoginFailed",
            "Login attempt failed.",
            (int)HttpStatusCode.Unauthorized);

    public static Error TooManyLoginAttempts =>
        new("Auth.TooManyLoginAttempts",
            "Too many login attempts. Try again later.",
            (int)HttpStatusCode.TooManyRequests);

    public static Error AccountLockedDueToFailures =>
        new("Auth.AccountLockedDueToFailures",
            "Account locked due to multiple failed login attempts.",
            (int)HttpStatusCode.Forbidden);


    public static Error TokenExpired =>
    new("Auth.TokenExpired",
        "Authentication token has expired.",
        (int)HttpStatusCode.Unauthorized);

    public static Error TokenMissing =>
        new("Auth.TokenMissing",
            "Authentication token is missing.",
            (int)HttpStatusCode.Unauthorized);

    public static Error TokenRevoked =>
        new("Auth.TokenRevoked",
            "Token has been revoked.",
            (int)HttpStatusCode.Unauthorized);

    public static Error TokenBlacklisted =>
        new("Auth.TokenBlacklisted",
            "Token is blacklisted.",
            (int)HttpStatusCode.Unauthorized);

    public static Error RefreshTokenAlreadyUsed =>
    new("Auth.RefreshTokenAlreadyUsed",
        "Refresh token has already been used.",
        (int)HttpStatusCode.Unauthorized);

    public static Error RefreshTokenRevoked =>
        new("Auth.RefreshTokenRevoked",
            "Refresh token has been revoked.",
            (int)HttpStatusCode.Unauthorized);

    public static Error RefreshTokenInvalid =>
        new("Auth.RefreshTokenInvalid",
            "Refresh token is invalid.",
            (int)HttpStatusCode.Unauthorized);

    public static Error EmailConfirmationTokenInvalid =>
    new("Auth.EmailConfirmationTokenInvalid",
        "Email confirmation token is invalid.",
        (int)HttpStatusCode.BadRequest);

    public static Error EmailConfirmationTokenExpired =>
        new("Auth.EmailConfirmationTokenExpired",
            "Email confirmation token has expired.",
            (int)HttpStatusCode.BadRequest);

    public static Error EmailConfirmationFailed =>
        new("Auth.EmailConfirmationFailed",
            "Email confirmation failed.",
            (int)HttpStatusCode.BadRequest);

    public static Error PasswordResetTokenInvalid =>
    new("Auth.PasswordResetTokenInvalid",
        "Password reset token is invalid.",
        (int)HttpStatusCode.BadRequest);

    public static Error PasswordResetTokenExpired =>
        new("Auth.PasswordResetTokenExpired",
            "Password reset token has expired.",
            (int)HttpStatusCode.BadRequest);

    public static Error PasswordResetFailed =>
        new("Auth.PasswordResetFailed",
            "Password reset failed.",
            (int)HttpStatusCode.BadRequest);


    public static Error AccessDenied =>
    new("Auth.AccessDenied",
        "You do not have permission to access this resource.",
        (int)HttpStatusCode.Forbidden);

    public static Error RoleNotFound =>
        new("Auth.RoleNotFound",
            "Role does not exist.",
            (int)HttpStatusCode.NotFound);

    public static Error RoleAlreadyAssigned =>
        new("Auth.RoleAlreadyAssigned",
            "User already has this role.",
            (int)HttpStatusCode.Conflict);

    public static Error RoleAssignmentFailed =>
        new("Auth.RoleAssignmentFailed",
            "Failed to assign role to user.",
            (int)HttpStatusCode.BadRequest);

    public static Error PermissionDenied =>
        new("Auth.PermissionDenied",
            "Permission denied.",
            (int)HttpStatusCode.Forbidden);

    public static Error InvalidCredentials =>
        new("Auth.InvalidCredentials",
            "Invalid email or password.",
            (int)HttpStatusCode.Unauthorized);

    public static Error InvaildToken=>
        new("Auth.InvaildToken",
            "Invalid authentication token.",
            (int)HttpStatusCode.Unauthorized);
}