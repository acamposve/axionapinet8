using Domain.Commands;
using Domain.Dtos;
using FluentResults;

namespace Core.Abstractions.Services;

public interface ICategoryService
{
    Task<Result<CategoryDto>> GetCategory(GetCategoryCommand getCategoryCommand, CancellationToken cancellationToken);
    Task<Result<int>> CreateCategory(CreateCategoryCommand createCategoryCommand, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateCategory(UpdateCategoryCommand updateCategoryCommand, CancellationToken cancellationToken);


}
