using Ardalis.GuardClauses;
using Core.Abstractions.Repositories;
using FluentResults;
using Infrastructure.Abstractions;
using Infrastructure.Database.EFContext;
using Infrastructure.Database.EFContext.Entities;
using Infrastructure.Resources;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

internal sealed class RoleQueriesRepository : IRoleQueriesRepository, ISecretRoleQueriesRepository
{
    private readonly UserContext _context;
    public RoleQueriesRepository(UserContext userContext)
    {
        _context = Guard.Against.Null(userContext);
    }

    public async Task<IEnumerable<RoleEntity>> GetRoleEntities(CancellationToken cancellationToken)
    {
        return await _context.Roles.ToListAsync(cancellationToken);
    }

    public async Task<Result<RoleEntity>> GetRoleEntity(string roleName, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.SingleOrDefaultAsync(x => x.Role.Equals(roleName, StringComparison.OrdinalIgnoreCase));
        return role is not null ? Result.Ok(role) : Result.Fail(string.Format(DatabaseErrorMessages.RoleNotExist, roleName));
    }

    public async Task<IEnumerable<string>> GetRoles(CancellationToken cancellationToken)
    {
        return await _context.Roles.Select(x => x.Role).ToListAsync(cancellationToken);
    }
}