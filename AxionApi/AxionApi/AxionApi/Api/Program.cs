using Api.Configuration;
using Api.EndpointBuilders;
using SmallApiToolkit.Extensions;
using SmallApiToolkit.Middleware;
using Core.Configuration;

using Infrastructure.Configuration;
using Serilog;
using Api.Middleware;



var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

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

//await app.Services.AddDefaultRoles();
//await app.Services.AddDefaultUsers();

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseSerilogRequestLogging();
app.Run();
