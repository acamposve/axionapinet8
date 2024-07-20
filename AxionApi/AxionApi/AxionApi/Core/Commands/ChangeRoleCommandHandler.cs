﻿using Ardalis.GuardClauses;
using Core.Abstractions.Commands;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Core.Resources;
using Domain.Commands;
using SmallApiToolkit.Core.Extensions;
using SmallApiToolkit.Core.Response;
using Validot;

namespace Core.Commands;

internal sealed class ChangeRoleCommandHandler : IChangeRoleCommandHandler
{
    private readonly IUserService _userService;
    private readonly IRoleQueriesRepository _roleQueriesRepository;
    private readonly IValidator<ChangeRoleCommand> _validator;

    public ChangeRoleCommandHandler(IUserService userService, IRoleQueriesRepository roleQueriesRepository, IValidator<ChangeRoleCommand> validator)
    {
        _userService = Guard.Against.Null(userService);
        _roleQueriesRepository = Guard.Against.Null(roleQueriesRepository);
        _validator = Guard.Against.Null(validator);
    }

    public async Task<HttpDataResponse<bool>> HandleAsync(ChangeRoleCommand request, CancellationToken cancellationToken)
    {
        if (!_validator.IsValid(request))
        {
            return HttpDataResponses.AsBadRequest<bool>(ErrorMessages.InvalidRequest);
        }

        var roles = await _roleQueriesRepository.GetRoles(cancellationToken);

        if (!roles.Contains(request.RoleName, StringComparer.OrdinalIgnoreCase))
        {
            return HttpDataResponses.AsBadRequest<bool>(ErrorMessages.FailedRoleChange);
        }

        var changeResult = await _userService.ChangeUserRole(request, cancellationToken);

        if (changeResult.IsFailed)
        {
            return HttpDataResponses.AsInternalServerError<bool>(ErrorMessages.FailedRoleChange);
        }

        return HttpDataResponses.AsOK(changeResult.Value);
    }
}