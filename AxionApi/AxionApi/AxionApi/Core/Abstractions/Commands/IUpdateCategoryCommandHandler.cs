using Domain.Commands;
using SmallApiToolkit.Core.RequestHandlers;

namespace Core.Abstractions.Commands;

public interface IUpdateCategoryCommandHandler : IHttpRequestHandler<bool, UpdateCategoryCommand>
{
}
