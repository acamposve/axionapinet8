using Core.Abstractions.Repositories;
using FluentResults;
using Infrastructure.Abstractions;
using Infrastructure.Database.EFContext.Entities;
using Infrastructure.Database.EFContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

internal sealed class CategoryCommandsRepository : ICategoryCommandsRepository, ISecretCategoryCommandsRepository
{
    private readonly UserContext _context;
    public CategoryCommandsRepository(UserContext userContext)
    {
        _context = userContext;
    }
    public async Task<int> AddCategory(CategoryEntity categoryEntity, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Categories.AddAsync(categoryEntity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {

            throw ex;
        }

        return categoryEntity.Id;
    }

    public async Task<Result<bool>> UpdateCategory(int Id, CategoryEntity category, CancellationToken cancellationToken)
    {
        try
        {
            var existingCategory = await _context.Categories.FindAsync(new object[] { Id }, cancellationToken);

            if (existingCategory == null)
            {
                return Result.Fail("Category not found");
            }

            // Update properties
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.Image = category.Image;

            // Asigna otros campos necesarios de category a existingCategory

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(true);
        }
        catch (DbUpdateException ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
