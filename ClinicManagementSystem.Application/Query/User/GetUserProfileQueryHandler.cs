using ClinicManagementSystem.Application.Abstractions.User;
using ClinicManagementSystem.Application.Dtos.User;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Query.User;

public class GetUserProfileQueryHandler(IUserService userService) : IRequestHandler<GetUserProfileQuery, Result<UserProfileDto>>
{
    private readonly IUserService _userService = userService;

    public async Task<Result<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    =>await _userService.GetUserProfileAsync(request.UserId);
}
