using Domain.Dtos;
using Infrastructure.Database.EFContext;
using Infrastructure.Database.EFContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class CategoryRepository : Repository<CategoryDto>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Implementación del método para obtener categorías con productos
    public async Task<IEnumerable<CategoryEntity>> GetCategoriesWithProductsAsync()
    {
        return await _context.Categories.Include(c => c.Products).ToListAsync();
    }

    // Implementación del método para obtener una categoría por nombre
    public async Task<CategoryEntity> GetCategoryByNameAsync(string name)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
    }
    public async Task AddCategoryAsync(CategoryEntity category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }
}
