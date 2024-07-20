using Core.Abstractions.Commands;
using Core.Abstractions.Queries;
using Core.Commands;
using Core.Extensions;
using Core.Queries;
using Core.Validation;
using Domain.Commands;
using Microsoft.Extensions.DependencyInjection;
using Validot;

namespace Core.Configuration;

public static class ContainerConfigurationExtension
{
    public static IServiceCollection AddCore(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddCoreHandlers()
            .AddCoreValidation();

    private static IServiceCollection AddCoreHandlers(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddScoped<ILoginCommandHandler, LoginCommandHandler>()
            .AddScoped<IRegisterCommandHandler, RegisterCommandHandler>()
            .AddScoped<IChangeRoleCommandHandler, ChangeRoleCommandHandler>()
            .AddScoped<IGetUserInfoQueryHandler, GetUserInfoQueryHandler>()
            .AddScoped<IGetServiceInfoQueryHandler, GetServiceInfoQueryHandler>();

    private static IServiceCollection AddCoreValidation(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddValidotSingleton<IValidator<ChangeRoleCommand>, ChangeRoleCommandSpecificationHolder, ChangeRoleCommand>()
            .AddValidotSingleton<IValidator<RegisterCommand>, RegisterCommandSpecificationHolder, RegisterCommand>()
            .AddValidotSingleton<IValidator<LoginCommand>, LoginCommandSpecificationHandler, LoginCommand>();
}
