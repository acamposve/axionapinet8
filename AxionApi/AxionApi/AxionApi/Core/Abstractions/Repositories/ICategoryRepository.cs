using Domain.Dtos;

namespace Core.Abstractions.Repositories;

public interface ICategoryRepository : IRepository<CategoryDto>
{
    Task<IEnumerable<CategoryDto>> GetCategoriesWithProductsAsync();
    Task<CategoryDto> GetCategoryByNameAsync(string name);
    Task AddCategoryAsync(CategoryDto category);
}
