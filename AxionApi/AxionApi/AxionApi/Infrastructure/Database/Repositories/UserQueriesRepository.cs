using Ardalis.GuardClauses;
using Core.Abstractions.Repositories;
using Domain.Dtos;
using FluentResults;
using Infrastructure.Abstractions;
using Infrastructure.Database.EFContext.Entities;
using Infrastructure.Database.EFContext;
using Infrastructure.Resources;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace Infrastructure.Database.Repositories;

internal sealed class UserQueriesRepository : IUserQueriesRepository, ISecretUserQueriesRepository
{
    private readonly UserContext _context;
    public UserQueriesRepository(UserContext userContext)
    {
        _context = Guard.Against.Null(userContext);
    }
    public async Task<Result<UserEntity>> FindUser(string username, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Username.Equals(username));
        return GetUserResult(user);
    }

    public async Task<Result<UserEntity>> GetUser(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Id.Equals(id));
        return GetUserResult(user);
    }

    public async Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(x => x.Role)
            .Select(x => x.Adapt<UserDto>()).ToListAsync();
    }

    private Result<UserEntity> GetUserResult(UserEntity? user)
    {
        return user is not null ? Result.Ok(user) : Result.Fail<UserEntity>(DatabaseErrorMessages.UserNotExist);
    }
}