using Domain.Commands;
using Domain.Dtos;
using FluentResults;

namespace Core.Abstractions.Services;

public interface IAccountService
{
    Task<Result<UserDto>> FindUser(LoginCommand loginCommand, CancellationToken cancellationToken);
    Task<Result<int>> CreateNewUser(RegisterCommand registerCommand, CancellationToken cancellationToken);
}
