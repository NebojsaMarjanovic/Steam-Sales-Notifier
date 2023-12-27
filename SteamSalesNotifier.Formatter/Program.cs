using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SteamSalesNotifier.Formatter;
using SteamSalesNotifier.Formatter.Configurations;
using SteamSalesNotifier.Formatter.Contracts;
using SteamSalesNotifier.Formatter.Implementations;
using SteamSalesNotifier.Formatter.Messaging;
using SteamSalesNotifier.Shared.Models.Output;
using SteamSalesNotifier.Shared.RabbitMq.Configurations;
using SteamSalesNotifier.Shared.RabbitMq.Contracts;
using SteamSalesNotifier.Shared.RabbitMq.Extensions;
using SteamSalesNotifier.Shared.RabbitMq.Serialization;

var builder = Host.CreateApplicationBuilder(args);

//options
builder.Services.AddOptions<MailTemplateOptions>().Bind(builder.Configuration.GetSection("MailTemplate"));
builder.Services.AddOptions<SenderChannelConfiguration>().Bind(builder.Configuration
           .GetSection("RabbitMqConfiguration:FormatterSenderChannel"));
builder.Services.AddOptions<ReceiverChannelConfiguration>().Bind(builder.Configuration
           .GetSection("RabbitMqConfiguration:FormatterReceiverChannel"));
//messaging
builder.Services.AddRabbitMq();
builder.Services.AddSingleton<IReceiverChannel<SteamSale>, ReceiverChannel>();
builder.Services.AddSingleton<ISenderChannel<MailNotification>, SenderChannel>();
builder.Services.AddSingleton<IMessageSerializer<SteamSale>, MessageSerializer<SteamSale>>();
builder.Services.AddSingleton<IMessageSerializer<MailNotification>, MessageSerializer<MailNotification>>();


//services
builder.Services.AddSingleton<IFormatterService, FormatterService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
