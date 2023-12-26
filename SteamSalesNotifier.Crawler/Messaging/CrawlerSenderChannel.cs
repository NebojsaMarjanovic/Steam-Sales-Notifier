using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SteamSalesNotifier.Shared.Models.Output;
using SteamSalesNotifier.Shared.RabbitMq.Configurations;
using SteamSalesNotifier.Shared.RabbitMq.Implementations;
using SteamSalesNotifier.Shared.RabbitMq.Serialization;

namespace SteamSalesNotifier.Crawler.Messaging
{
    public class CrawlerSenderChannel : SenderChannel<SteamSale>
    {
        public CrawlerSenderChannel(IConnection connection, IOptions<SenderChannelConfiguration> configuration, 
            IMessageSerializer<SteamSale> messageSerializer) : base(connection, configuration.Value, messageSerializer)
        {
        }
    }
}
