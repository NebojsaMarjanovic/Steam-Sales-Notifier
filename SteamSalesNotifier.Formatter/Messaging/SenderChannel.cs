using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SteamSalesNotifier.Shared.Models.Output;
using SteamSalesNotifier.Shared.RabbitMq.Configurations;
using SteamSalesNotifier.Shared.RabbitMq.Implementations;
using SteamSalesNotifier.Shared.RabbitMq.Serialization;

namespace SteamSalesNotifier.Formatter.Messaging
{
    internal class SenderChannel : SenderChannel<MailNotification>
    {
        public SenderChannel(IConnection connection, IOptions<SenderChannelConfiguration> configuration, IMessageSerializer<MailNotification> messageSerializer) 
            : base(connection, configuration.Value, messageSerializer)
        {
        }
    }
}
