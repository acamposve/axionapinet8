using Domain.Dtos;

namespace Core.Abstractions.Repositories;

public interface ICategoryQueriesRepository
{
    Task<IEnumerable<CategoryDto>> GetCategories(CancellationToken cancellationToken);
}
