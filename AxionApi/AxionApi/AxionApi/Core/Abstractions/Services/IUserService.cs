using Domain.Commands;
using FluentResults;

namespace Core.Abstractions.Services;

public interface IUserService
{
    Task<Result<bool>> ChangeUserRole(ChangeRoleCommand changeRoleCommand, CancellationToken cancellationToken);
}