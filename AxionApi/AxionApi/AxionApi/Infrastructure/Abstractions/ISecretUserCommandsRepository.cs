using FluentResults;
using Infrastructure.Database.EFContext.Entities;

namespace Infrastructure.Abstractions;

internal interface ISecretUserCommandsRepository
{
    Task<int> AddUser(UserEntity userEntity, CancellationToken cancellationToken);
    Task<Result<bool>> ChangeUserRole(UserEntity user, RoleEntity role, CancellationToken cancellationToken);
}