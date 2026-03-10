using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ClinicManagementSystem.Domain.Abstractions;

public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationPipelineBehavior<TRequest, TResponse>> _logger;

    public ValidationPipelineBehavior(
        IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidationPipelineBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
        {
            var error = new Error(
                "Validation.Error",
                failures.First().ErrorMessage,
                400
            );

            // التحقق من نوع TResponse
            if (typeof(TResponse).IsGenericType)
            {
                // استخراج النوع العام (LoginResponseDto)
                var genericArgumentType = typeof(TResponse).GetGenericArguments()[0];

                _logger.LogWarning($"Creating Result<{genericArgumentType.Name}> with error");

                // البحث عن طريقة Failure العامية
                var failureMethod = typeof(Result)
                    .GetMethods()
                    .First(m => m.Name == "Failure" && m.IsGenericMethod);

                // إنشاء طريقة Failure مخصصة للنوع المطلوب
                var genericFailureMethod = failureMethod.MakeGenericMethod(genericArgumentType);

                // استدعاء الطريقة وإنشاء Result<LoginResponseDto>
                var failureResult = genericFailureMethod.Invoke(null, new object[] { error });

                return (TResponse)failureResult!;
            }
            else
            {
                // إذا كان TResponse غير عامي
                return (TResponse)(object)Result.Failure(error);
            }
        }

        return await next();
    }
}