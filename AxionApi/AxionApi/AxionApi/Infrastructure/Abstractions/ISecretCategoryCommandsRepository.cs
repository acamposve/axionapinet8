using FluentResults;
using Infrastructure.Database.EFContext.Entities;

namespace Infrastructure.Abstractions;

internal interface ISecretCategoryCommandsRepository
{
    Task<int> AddCategory(CategoryEntity categoryEntity, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateCategory(int Id, CategoryEntity category, CancellationToken cancellationToken);

}
