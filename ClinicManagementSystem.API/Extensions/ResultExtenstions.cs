using ClinicManagementSystem.API.Responses;
using ClinicManagementSystem.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

public static class ResultExtensions
{
    
    public static IActionResult ToApiResponse<T>(
        this Result<T> result,
        string successMessage = "",
        string failMessage = "")
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(
                ApiResponse<T>.SuccessResponse(
                    result.Value,
                    string.IsNullOrWhiteSpace(successMessage)
                        ? "Operation completed successfully"
                        : successMessage)
            );
        }

        return new BadRequestObjectResult(
            ApiResponse<T>.FailResponse(
                string.IsNullOrWhiteSpace(failMessage)
                    ? result.Error.Description
                    : failMessage
            )
        );
    }

    
    public static IActionResult ToApiResponse(
        this Result result,
        string successMessage = "",
        string failMessage = "")
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(
                ApiResponse<string>.SuccessResponse(
                    "Success", 
                    string.IsNullOrWhiteSpace(successMessage)
                        ? "Operation completed successfully"
                        : successMessage)
            );
        }

        return new BadRequestObjectResult(
            ApiResponse<string>.FailResponse(
                string.IsNullOrWhiteSpace(failMessage)
                    ? result.Error.Description
                    : failMessage
            )
        );
    }
}