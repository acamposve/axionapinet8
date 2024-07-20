using Domain.Commands;
using SmallApiToolkit.Core.RequestHandlers;

namespace Core.Abstractions.Commands;

public interface IChangeRoleCommandHandler : IHttpRequestHandler<bool, ChangeRoleCommand>
{
}