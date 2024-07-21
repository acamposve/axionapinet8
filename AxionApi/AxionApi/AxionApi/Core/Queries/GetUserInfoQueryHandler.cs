using Core.Abstractions.Queries;
using Core.Abstractions.Repositories;
using Domain.Dtos;
using SmallApiToolkit.Core.Extensions;
using SmallApiToolkit.Core.Response;

namespace Core.Queries;

internal sealed class GetUserInfoQueryHandler : IGetUserInfoQueryHandler
{
    private readonly IUserQueriesRepository _userQueriesRepository;

    public GetUserInfoQueryHandler(IUserQueriesRepository userQueriesRepository)
    {
        _userQueriesRepository = userQueriesRepository;
    }
    public async Task<HttpDataResponse<IEnumerable<UserDto>>> HandleAsync(EmptyRequest request, CancellationToken cancellationToken)
    {
        var users = await _userQueriesRepository.GetUsers(cancellationToken);
        return HttpDataResponses.AsOK(users);
    }
}