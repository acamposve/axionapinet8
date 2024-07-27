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

internal sealed class CategoryQueriesRepository : ICategoryQueriesRepository, ISecretCategoryQueriesRepository
{
    private readonly UserContext _context;
    public CategoryQueriesRepository(UserContext userContext)
    {
        _context = userContext;
    }
    public async Task<Result<CategoryEntity>> FindCategory(string name, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .Include(x => x.Products)
            .SingleOrDefaultAsync(x => x.Name.Equals(name));
        return GetCategoryResult(category);
    }

    public async Task<Result<CategoryEntity>> GetCategory(int id, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .Include(x => x.Products)
            .SingleOrDefaultAsync(x => x.Id.Equals(id));
        return GetCategoryResult(category);
    }

    public async Task<IEnumerable<CategoryDto>> GetCategories(CancellationToken cancellationToken)
    {
        return await _context.Categories
            .Include(x => x.Products)
            .Select(x => x.Adapt<CategoryDto>()).ToListAsync();
    }

    private Result<CategoryEntity> GetCategoryResult(CategoryEntity? category)
    {
        return category is not null ? Result.Ok(category) : Result.Fail<CategoryEntity>(DatabaseErrorMessages.CategoryNotExist);
    }
}
