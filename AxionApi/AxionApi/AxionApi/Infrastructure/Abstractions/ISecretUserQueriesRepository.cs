using FluentResults;
using Infrastructure.Database.EFContext.Entities;

namespace Infrastructure.Abstractions;

internal interface ISecretUserQueriesRepository
{
    Task<Result<UserEntity>> GetUser(int id, CancellationToken cancellationToken);
    Task<Result<UserEntity>> FindUser(string username, CancellationToken cancellationToken);
}