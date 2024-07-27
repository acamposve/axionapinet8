using Core.Abstractions.Repositories;
using FluentResults;
using Infrastructure.Abstractions;
using Infrastructure.Database.EFContext;
using Infrastructure.Database.EFContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

internal sealed class UserCommandsRepository : IUserCommandsRepository, ISecretUserCommandsRepository
{
    private readonly UserContext _context;
    public UserCommandsRepository(UserContext userContext)
    {
        _context = userContext;
    }
    public async Task<int> AddUser(UserEntity userEntity, CancellationToken cancellationToken)
    {
        try
        {
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {

            throw ex;
        }

        return userEntity.Id;
    }

    public async Task<Result<bool>> ChangeUserRole(UserEntity user, RoleEntity role, CancellationToken cancellationToken)
    {
        try
        {
            user.Role = role;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(true);
        }
        catch (DbUpdateException ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}