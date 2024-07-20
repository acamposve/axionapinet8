using Api.Configuration;
using Api.EndpointBuilders;
using SmallApiToolkit.Extensions;
using SmallApiToolkit.Middleware;
using Core.Configuration;

using Infrastructure.Configuration;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();

builder.Services
    .AddCore()
    .AddInfrastructure(builder.Configuration);




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

app.MapVersionGroup(1)
   .BuildUserEndpoints()
   .BuildServiceEndpoints();

await app.Services.AddDefaultRoles();
await app.Services.AddDefaultUsers();

app.Run();
