using ClinicManagementSystem.Application.Commands.Authentication.Login;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ClinicManagementSystem.Application.Extensions;

public static class ServiceCollectionExtenstions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Add MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            // تأكد من ترتيب الـ behaviors
            cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        // Add FluentValidation - تأكد من تسجيل جميع الـ validators
        services.AddValidatorsFromAssembly(assembly, ServiceLifetime.Scoped);

        // OR يمكنك إضافة这种行为 للتأكد
        services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>(ServiceLifetime.Scoped);





        return services;
    }
}