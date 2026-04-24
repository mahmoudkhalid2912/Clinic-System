using ClinicManagementSystem.Application.Extensions;
using ClinicManagementSystem.Infrastructure.Extensions;
using ClinicManagementSystem.Infrastructure.SeedData;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var scheduleSeeder = scope.ServiceProvider.GetRequiredService<ScheduleSeederService>();
    await scheduleSeeder.SeedAsync();
}


app.UseCors("AllowAll");

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.Run();