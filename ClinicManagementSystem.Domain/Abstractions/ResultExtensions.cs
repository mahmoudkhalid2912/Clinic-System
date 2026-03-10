using Microsoft.AspNetCore.Mvc;
using ClinicManagementSystem.Domain.Abstractions;

public static class ResultExtensions
{
    public static ObjectResult ToSimpleError(this Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("Cannot convert success result to problem");

        var error = result.Error!;

        var simpleError = new
        {
            code = error.Code,
            message = error.Description,
            statusCode = error.StatusCode ?? 400
        };

        return new ObjectResult(simpleError)
        {
            StatusCode = simpleError.statusCode
        };
    }
}