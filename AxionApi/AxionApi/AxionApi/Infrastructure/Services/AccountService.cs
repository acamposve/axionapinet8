using Core.Abstractions.Services;
using Core.Resources;
using Domain.Commands;
using Domain.Dtos;
using FluentResults;
using Infrastructure.Abstractions;
using Infrastructure.Database.EFContext.Entities;
using Infrastructure.Extensions;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

internal sealed class AccountService : IAccountService
{
    private readonly ISecretUserQueriesRepository _secretUserQueriesRepository;
    private readonly ISecretUserCommandsRepository _secretUserCommandsRepository;
    private readonly ILogger<AccountService> _logger; 


    public AccountService(ISecretUserQueriesRepository secretUserQueriesRepository, 
        ISecretUserCommandsRepository secretUserCommandsRepository,
        ILogger<AccountService> logger)
    {
        _secretUserQueriesRepository = secretUserQueriesRepository;
        _secretUserCommandsRepository = secretUserCommandsRepository;
        _logger = logger;
    }
    public async Task<Result<UserDto>> FindUser(LoginCommand loginCommand, CancellationToken cancellationToken)
    {
        var userEntityResult = await _secretUserQueriesRepository.FindUser(loginCommand.Username, cancellationToken);
        if (userEntityResult.IsFailed)
        {
            return Result.Fail<UserDto>(ErrorMessages.InvalidUsernameOrPassword);
        }
        _logger.LogInformation($"user entity result  { userEntityResult}" );
        var isPasswordVerified = PasswordHasher.VerifyPassword(loginCommand.Password, userEntityResult.Value.Password);
        _logger.LogInformation($"password verified  {isPasswordVerified}");
        if (isPasswordVerified)
        {
            return Result.Ok(userEntityResult.Value.Adapt<UserDto>());
        }

        return Result.Fail<UserDto>(ErrorMessages.InvalidUsernameOrPassword);

    }

    public async Task<Result<int>> CreateNewUser(RegisterCommand registerCommand, CancellationToken cancellationToken)
    {
        var userEntityResult = await _secretUserQueriesRepository.FindUser(registerCommand.Username, cancellationToken);
        
        
        if (userEntityResult.IsSuccess)
        {
            return Result.Fail<int>(ErrorMessages.InvalidUsernameOrEmail);
        }
        _logger.LogInformation($"user entity result  {userEntityResult}");
        var userEntity = registerCommand.Adapt<UserEntity>();
        _logger.LogInformation($"user entity result  {userEntity}");
        var userId = await _secretUserCommandsRepository.AddUser(userEntity, cancellationToken);

        return Result.Ok(userId);
    }
}