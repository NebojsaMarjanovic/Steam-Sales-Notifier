using SteamSalesNotifier.MailSender;
using SteamSalesNotifier.MailSender.Contracts;
using SteamSalesNotifier.MailSender.Implementations;
using SteamSalesNotifier.MailSender.Messaging;
using SteamSalesNotifier.Shared.Models.Output;
using SteamSalesNotifier.Shared.RabbitMq.Configurations;
using SteamSalesNotifier.Shared.RabbitMq.Contracts;
using SteamSalesNotifier.Shared.RabbitMq.Extensions;
using SteamSalesNotifier.Shared.RabbitMq.Serialization;

var builder = Host.CreateApplicationBuilder(args);

//options
builder.Services.AddOptions<ReceiverChannelConfiguration>().Bind(builder.Configuration
    .GetSection("RabbitMqConfiguration:SenderReceiverChannel"));

//messaging
builder.Services.AddRabbitMq();
builder.Services.AddSingleton<IReceiverChannel<MailNotification>, ReceiverChannel>();
builder.Services.AddSingleton<IMessageSerializer<MailNotification>, MessageSerializer<MailNotification>>();


//services
builder.Services.AddSingleton<IMailSender, MailSender>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

//ailr msrm wycb nkrn 