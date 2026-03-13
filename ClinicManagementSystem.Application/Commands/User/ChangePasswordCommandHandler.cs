using ClinicManagementSystem.Application.Abstractions.User;
using ClinicManagementSystem.Domain.Abstractions;
using MediatR;

namespace ClinicManagementSystem.Application.Commands.User;

public class ChangePasswordCommandHandler(IUserService userService) : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly IUserService _userService = userService;

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    => await _userService.ChangePasswordAsync(request.UserId, request);
}
