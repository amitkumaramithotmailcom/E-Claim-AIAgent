using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<ClaimApiClient>();
builder.Services.AddSingleton<UserApiClient>();

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<ClaimTools>()
    .WithTools<UserTools>();

var app = builder.Build();

await app.RunAsync();