using Domain.Dtos;
using FluentResults;
using Infrastructure.Database.EFContext.Entities;

namespace Infrastructure.Abstractions;

internal interface ISecretCategoryQueriesRepository
{
    Task<Result<CategoryEntity>> GetCategory(int id, CancellationToken cancellationToken);
    Task<Result<CategoryEntity>> FindCategory(string name, CancellationToken cancellationToken);


}
