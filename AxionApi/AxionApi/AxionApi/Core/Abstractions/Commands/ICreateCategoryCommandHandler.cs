using Domain.Commands;
using SmallApiToolkit.Core.RequestHandlers;

namespace Core.Abstractions.Commands;

public interface ICreateCategoryCommandHandler : IHttpRequestHandler<bool, CreateCategoryCommand>
{
}
