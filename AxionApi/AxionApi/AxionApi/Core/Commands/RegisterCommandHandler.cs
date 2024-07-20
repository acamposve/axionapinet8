using Ardalis.GuardClauses;
using Core.Abstractions.Commands;
using Core.Abstractions.Services;
using Core.Resources;
using Domain.Commands;
using SmallApiToolkit.Core.Extensions;
using SmallApiToolkit.Core.Response;
using Validot;

namespace Core.Commands;

internal sealed class RegisterCommandHandler : IRegisterCommandHandler
{
    private readonly IAccountService _accountService;
    private readonly IValidator<RegisterCommand> _validator;

    public RegisterCommandHandler(IAccountService accountService, IValidator<RegisterCommand> validator)
    {
        _accountService = Guard.Against.Null(accountService);
        _validator = Guard.Against.Null(validator);
    }
    public async Task<HttpDataResponse<bool>> HandleAsync(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (!_validator.IsValid(request))
        {
            return HttpDataResponses.AsBadRequest<bool>(ErrorMessages.InvalidRequest);
        }

        var userResult = await _accountService.CreateNewUser(request, cancellationToken);

        if (userResult.IsFailed)
        {
            return HttpDataResponses.AsBadRequest<bool>(ErrorMessages.InvalidRegistration);
        }

        return HttpDataResponses.AsOK(true);
    }
}