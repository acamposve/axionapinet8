using Domain.Extensions;
using Infrastructure.Abstractions;
using Infrastructure.Database.EFContext;
using Infrastructure.Database.EFContext.Entities;
namespace Infrastructure.Database.Repositories;

internal sealed class RoleCommandRepository : ISecretRoleCommandRepository
{
    private readonly UserContext _context;
    public RoleCommandRepository(UserContext userContext)
    {
        _context = userContext;
    }

    public async Task AddRoles(IEnumerable<string> roles, CancellationToken cancellationToken)
    {
        await roles.ForEachAsync(async (role) => await _context.Roles.AddAsync(new RoleEntity { Role = role }, cancellationToken));
        await _context.SaveChangesAsync(cancellationToken);
    }
}