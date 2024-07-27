using Domain.Commands;
using Domain.Dtos;
using SmallApiToolkit.Core.RequestHandlers;

namespace Core.Abstractions.Commands;

public interface IGetCategoryCommandHandler : IHttpRequestHandler<CategoryDto, GetCategoryCommand>
{
}
