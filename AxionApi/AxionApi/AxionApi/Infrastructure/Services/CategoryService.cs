using Core.Abstractions.Services;
using Core.Resources;
using Domain.Commands;
using Domain.Dtos;
using FluentResults;
using Infrastructure.Abstractions;
using Infrastructure.Database.EFContext.Entities;
using Mapster;

namespace Infrastructure.Services;

internal sealed class CategoryService : ICategoryService
{
    private readonly ISecretCategoryQueriesRepository _secretCategoryQueriesRepository;
    private readonly ISecretCategoryCommandsRepository _secretCategoryCommandsRepository;

    public CategoryService(
        ISecretCategoryQueriesRepository secretCategoryQueriesRepository,
        ISecretCategoryCommandsRepository secretCategoryCommandsRepository)
    {
        _secretCategoryQueriesRepository = secretCategoryQueriesRepository;
        _secretCategoryCommandsRepository = secretCategoryCommandsRepository;
    }

    public async Task<Result<bool>> UpdateCategory(UpdateCategoryCommand updateCategoryCommand, CancellationToken cancellationToken)
    {
        var categoryResult = await _secretCategoryQueriesRepository.FindCategory(updateCategoryCommand.Name, cancellationToken);
        if (categoryResult.IsFailed)
        {
            return Result.Fail(categoryResult.Errors);
        }

        var categoryEntity = updateCategoryCommand.Adapt<CategoryEntity>();

        return await _secretCategoryCommandsRepository.UpdateCategory(categoryResult.Value.Id, categoryEntity, cancellationToken);
    }

    public async Task<Result<CategoryDto>> GetCategory(GetCategoryCommand getCategoryCommand, CancellationToken cancellationToken)
    {
        var categoryEntityResult = await _secretCategoryQueriesRepository.GetCategory(getCategoryCommand.Id, cancellationToken);
        if (categoryEntityResult.IsFailed)
        {
            return Result.Fail<CategoryDto>(ErrorMessages.InvalidUsernameOrPassword);
        }
        return Result.Ok(categoryEntityResult.Value.Adapt<CategoryDto>());
    }




    public async Task<Result<int>> CreateCategory(CreateCategoryCommand createCategoryCommand, CancellationToken cancellationToken)
    {
        var categoryEntityResult = await _secretCategoryQueriesRepository.FindCategory(createCategoryCommand.Name, cancellationToken);


        if (categoryEntityResult.IsSuccess)
        {
            return Result.Fail<int>(ErrorMessages.InvalidUsernameOrEmail);
        }


        var categoryEntity = createCategoryCommand.Adapt<CategoryEntity>();


        var categoryId = await _secretCategoryCommandsRepository.AddCategory(categoryEntity, cancellationToken);

        return Result.Ok(categoryId);
    }
}
