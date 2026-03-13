using ClinicManagementSystem.Application.Dtos.User;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Query.User;

public class GetUserProfileQuery:IRequest<Result<UserProfileDto>>
{
    public string UserId { get; set; }=string.Empty;
}
