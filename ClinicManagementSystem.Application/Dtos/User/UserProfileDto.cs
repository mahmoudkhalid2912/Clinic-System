using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Application.Dtos.User;

public class UserProfileDto
{
    public string FullName { get; set; }=string.Empty;
    public string Email { get; set; }=string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

}
