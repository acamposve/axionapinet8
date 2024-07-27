using Api.Configuration;
using Core.Abstractions.Commands;
using Core.Abstractions.Queries;
using Domain.Commands;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using SmallApiToolkit.Core.Extensions;
using SmallApiToolkit.Core.Response;

namespace Api.EndpointBuilders;

public static class CategoryEndpointBuilder
{
    public static IEndpointRouteBuilder BuildCategoryEndpointBuilder(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        return endpointRouteBuilder
            .MapGroup("category")
            .BuildCategoryEndpoints()
            .BuildUpdateCategoryEndpoints()
            .BuildCategoryInfoEndpoints();
    }
    private static IEndpointRouteBuilder BuildCategoryEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {


        endpointRouteBuilder.MapPost("createCategory",
            async (CreateCategoryCommand createCategoryCommand,
            [FromServices] ICreateCategoryCommandHandler createCategoryCommandHandler,
            CancellationToken cancellationToken) =>


            await createCategoryCommandHandler.SendAsync(createCategoryCommand, cancellationToken))
                .Produces<CategoryDto>()
                .WithName("CreateCategory")
                .RequireAuthorization(AuthorizationConfiguration.DeveloperPolicyName)
                .AllowAnonymous()
                .WithOpenApi();

        return endpointRouteBuilder;
    }

    private static IEndpointRouteBuilder BuildUpdateCategoryEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("updateCategory",
            async (UpdateCategoryCommand updateCategoryCommand, [FromServices] IUpdateCategoryCommandHandler updateCategoryCommandHandler, CancellationToken cancellationToken) =>
            await updateCategoryCommandHandler.SendAsync(updateCategoryCommand, cancellationToken))
                .Produces<bool>()
                .WithName("UpdateCategory")
                .RequireAuthorization(AuthorizationConfiguration.AdministratorPolicyName)
                .WithOpenApi();

        return endpointRouteBuilder;
    }

    private static IEndpointRouteBuilder BuildCategoryInfoEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("getCategory",
            async (GetCategoryCommand getCategoryCommand, [FromServices] IGetCategoryCommandHandler getCategoryCommandHandler, CancellationToken cancellationToken) =>
            await getCategoryCommandHandler.SendAsync(getCategoryCommand, cancellationToken))
                .Produces<CategoryDto>()
                .WithName("GetCategory")
                .AllowAnonymous()
                .WithOpenApi();

        endpointRouteBuilder.MapGet("getCategoriesInfo",
            async ([FromServices] IGetCategoryInfoQueryHandler getCategoryInfoCommandHandler, CancellationToken cancellationToken) =>
            await getCategoryInfoCommandHandler.SendAsync(EmptyRequest.Instance, cancellationToken))
                .Produces<IEnumerable<CategoryDto>>()
                .WithName("GetCategoriesInfo")

                .WithOpenApi();

        return endpointRouteBuilder;
    }
}
