using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SteamSalesNotifier.Crawler;
using SteamSalesNotifier.Crawler.Configuration;
using SteamSalesNotifier.Crawler.Contracts;
using SteamSalesNotifier.Crawler.Implementations;
using SteamSalesNotifier.Crawler.Mappers;
using SteamSalesNotifier.Crawler.Messaging;
using SteamSalesNotifier.Crawler.Models;
using SteamSalesNotifier.Crawler.Utilities.Serialization;
using SteamSalesNotifier.Shared.Models.Output;
using SteamSalesNotifier.Shared.RabbitMq.Configurations;
using SteamSalesNotifier.Shared.RabbitMq.Contracts;
using SteamSalesNotifier.Shared.RabbitMq.Extensions;
using SteamSalesNotifier.Shared.RabbitMq.Serialization;

string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
string folder = System.IO.Directory.GetParent(assemblyPath).FullName;
Environment.CurrentDirectory = folder;

var host = Host.CreateDefaultBuilder(args)
    .UseContentRoot(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location)!.FullName)
    .ConfigureServices((hostContext, services) =>
    {
        //Options
        services.AddOptions<SteamApiOptions>().Bind(hostContext.Configuration.GetSection("SteamApi"));
        services.AddOptions<SenderChannelConfiguration>().Bind(hostContext.Configuration
            .GetSection("RabbitMqConfiguration:SenderChannelConfiguration"));

        //Http
        services.AddHttpClient();

        //Mappers
        services.AddAutoMapper(typeof(GameProfile));

        //RabbitMq
        services.AddRabbitMq();
        services.AddSingleton<ISenderChannel<SteamSale>, CrawlerSenderChannel>();
        services.AddSingleton<IMessageSerializer<SteamSale>, MessageSerializer<SteamSale>>();

        //Services
        services.AddSingleton<ICrawler, Crawler>();
        services.AddSingleton<IJsonSerializer<Response>, JsonSerializer<Response>>();
        services.AddHostedService<Script>();

        //logging
        services.AddLogging();

    }).Build();

await host.RunAsync();