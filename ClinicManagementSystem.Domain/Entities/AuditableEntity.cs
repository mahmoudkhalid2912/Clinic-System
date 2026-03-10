namespace ClinicManagementSystem.Domain.Entities;

public class AuditableEntity
{
    public string CreatedById { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string? UpdatedById { get; set; }
    public DateTime? UpdatedOn { get; set; }

}
