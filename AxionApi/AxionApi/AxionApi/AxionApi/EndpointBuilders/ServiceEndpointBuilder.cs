using AxionApi.Configuration;
using Core.Abstractions.Queries;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using SmallApiToolkit.Core.Extensions;
using SmallApiToolkit.Core.Response;

namespace AxionApi.EndpointBuilders;

public static class ServiceEndpointBuilder
{
    public static IEndpointRouteBuilder BuildServiceEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        return endpointRouteBuilder
            .MapGroup("service")
            .BuildServiceInfoEndpoints();
    }
    private static IEndpointRouteBuilder BuildServiceInfoEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("getServiceInfo",
            async ([FromServices] IGetServiceInfoQueryHandler getServiceInfoQueryHandler, CancellationToken cancellationToken) =>
            await getServiceInfoQueryHandler.SendAsync(EmptyRequest.Instance, cancellationToken))
                .Produces<ServiceInfoDto>()
                .WithName("GetServiceInfo")
                .RequireAuthorization(AuthorizationConfiguration.UserPolicyName)
                .WithOpenApi();

        return endpointRouteBuilder;
    }


}