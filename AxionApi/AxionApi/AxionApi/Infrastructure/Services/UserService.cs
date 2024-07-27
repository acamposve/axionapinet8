using Core.Abstractions.Services;
using Domain.Commands;
using FluentResults;
using Infrastructure.Abstractions;

namespace Infrastructure.Services;

internal sealed class UserService : IUserService
{
    private readonly ISecretUserQueriesRepository _secretUserQueriesRepository;
    private readonly ISecretUserCommandsRepository _secretUserCommandsRepository;
    private readonly ISecretRoleQueriesRepository _secretRoleQueriesRepository;

    public UserService(
        ISecretUserQueriesRepository secretUserQueriesRepository,
        ISecretUserCommandsRepository secretUserCommandsRepository,
        ISecretRoleQueriesRepository secretRoleQueriesRepository)
    {
        _secretUserQueriesRepository = secretUserQueriesRepository;
        _secretUserCommandsRepository = secretUserCommandsRepository;
        _secretRoleQueriesRepository = secretRoleQueriesRepository;
    }

    public async Task<Result<bool>> ChangeUserRole(ChangeRoleCommand changeRoleCommand, CancellationToken cancellationToken)
    {
        var userResult = await _secretUserQueriesRepository.FindUser(changeRoleCommand.UserName, cancellationToken);
        if (userResult.IsFailed)
        {
            return Result.Fail(userResult.Errors);
        }

        var roleResult = await _secretRoleQueriesRepository.GetRoleEntity(changeRoleCommand.RoleName, cancellationToken);

        if (roleResult.IsFailed)
        {
            return Result.Fail(roleResult.Errors);
        }

        return await _secretUserCommandsRepository.ChangeUserRole(userResult.Value, roleResult.Value, cancellationToken);
    }
}