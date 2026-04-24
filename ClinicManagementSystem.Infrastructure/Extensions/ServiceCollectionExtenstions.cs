using ClinicManagementSystem.Application.Abstractions.Authentication;
using ClinicManagementSystem.Application.Abstractions.Payment;
using ClinicManagementSystem.Application.Abstractions.User;
using ClinicManagementSystem.Domain.Abstractions.IUnitOfWork;
using ClinicManagementSystem.Domain.Settings;
using ClinicManagementSystem.Infrastructure.Identity;
using ClinicManagementSystem.Infrastructure.persistence.UnitOfWork;
using ClinicManagementSystem.Infrastructure.Persistence;
using ClinicManagementSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.Extensions;

public static class ServiceCollectionExtenstions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ClinicDb");
        services.AddDbContext<ClinicDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddAuthentication(configuration);
        services.AddUserService();

        services.Configure<MailOptions>(configuration.GetSection(nameof(MailOptions)));

        services.Configure<StripeSettings>(configuration.GetSection(StripeSettings.SectionName));

        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IStripeService, StripeService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>(); 
        return services;
    }




    //Adding Authentication and Authorization services
    private static IServiceCollection AddAuthentication(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>().
             AddEntityFrameworkStores<ClinicDbContext>()
             .AddDefaultTokenProviders();

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddOptions<JwtSettings>()
            .Bind(configuration.GetSection(JwtSettings.SectionName)).
            ValidateDataAnnotations().ValidateOnStart();

        var JwtSetting= configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = JwtSetting?.Issuer,
                ValidAudience = JwtSetting?.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSetting?.SecretKey!))
            };
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
           // options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;
        });
        return services;
    }

    private static IServiceCollection AddUserService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }

}
