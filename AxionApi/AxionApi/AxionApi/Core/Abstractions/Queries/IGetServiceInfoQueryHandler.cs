using Domain.Dtos;
using SmallApiToolkit.Core.RequestHandlers;
using SmallApiToolkit.Core.Response;

namespace Core.Abstractions.Queries;
public interface IGetServiceInfoQueryHandler : IHttpRequestHandler<ServiceInfoDto, EmptyRequest>
{
}