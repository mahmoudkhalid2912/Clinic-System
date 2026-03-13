using ClinicManagementSystem.Domain.Abstractions;
using System.Net;


namespace ClinicManagementSystem.Domain.Errors;

public class UserError
{
    public static Error UserAlreadyExists =>
    new("Auth.UserAlreadyExists",
        "A user with this email already exists.",
        (int)HttpStatusCode.Conflict);

    public static Error UserNameAlreadyTaken =>
        new("Auth.UserNameAlreadyTaken",
            "Username is already taken.",
            (int)HttpStatusCode.Conflict);

    public static Error EmailAlreadyConfirmed =>
        new("Auth.EmailAlreadyConfirmed",
            "Email is already confirmed.",
            (int)HttpStatusCode.BadRequest);

    public static Error EmailNotConfirmed =>
        new("Auth.EmailNotConfirmed",
            "Email address has not been confirmed.",
            (int)HttpStatusCode.Forbidden);

    public static Error PhoneNumberAlreadyUsed =>
        new("Auth.PhoneNumberAlreadyUsed",
            "Phone number is already used.",
            (int)HttpStatusCode.Conflict);

    public static Error PhoneNumberNotConfirmed =>
        new("Auth.PhoneNumberNotConfirmed",
            "Phone number has not been confirmed.",
            (int)HttpStatusCode.Forbidden);

    public static Error UserDisabled =>
        new("Auth.UserDisabled",
            "User account has been disabled.",
            (int)HttpStatusCode.Forbidden);

    public static Error UserLocked =>
        new("Auth.UserLocked",
            "User account is locked.",
            (int)HttpStatusCode.Forbidden);

    public static Error UserDeleted =>
        new("Auth.UserDeleted",
            "User account has been deleted.",
            (int)HttpStatusCode.Gone);

        public static Error UserNotFound=>
        new("Auth.UserNotFound",
            "User account not found.",
            (int)HttpStatusCode.NotFound);

    public static Error DublicatedEmail=>
        new("Auth.DublicatedEmail",
            "Email is already used by another account.",
            (int)HttpStatusCode.Conflict);
     public static Error DublicatedPhoneNumber =>
        new("Auth.DublicatedPhoneNumber",
            "Phone number is already used by another account.",
            (int)HttpStatusCode.Conflict);

     public static Error InvalidCode=>
                new("Auth.InvalidCode",
            "The provided code is invalid.",
            (int)HttpStatusCode.BadRequest);
}
