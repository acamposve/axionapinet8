using FluentResults;
using Infrastructure.Database.EFContext.Entities;

namespace Infrastructure.Abstractions;

internal interface ISecretRoleQueriesRepository
{
    Task<IEnumerable<RoleEntity>> GetRoleEntities(CancellationToken cancellationToken);
    Task<Result<RoleEntity>> GetRoleEntity(string roleName, CancellationToken cancellationToken);
}