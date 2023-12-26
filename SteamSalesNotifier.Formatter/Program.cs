using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SteamSalesNotifier.Formatter;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
