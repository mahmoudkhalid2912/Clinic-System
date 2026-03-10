namespace ClinicManagementSystem.Application.Dtos.Authentication;

public class ApplicationUserDto
{
    public string Id { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string FullName { get; set; } = null!;

}
