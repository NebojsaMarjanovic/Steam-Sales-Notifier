using RabbitMQ.Client;
using SteamSalesNotifier.Shared.RabbitMq.Configurations;
using SteamSalesNotifier.Shared.RabbitMq.Contracts;
using SteamSalesNotifier.Shared.RabbitMq.Serialization;

namespace SteamSalesNotifier.Shared.RabbitMq.Implementations
{
    public class SenderChannel<T> : ISenderChannel<T> where T : class, IMessage
    {
        private readonly IMessageSerializer<T> _messageSerializer;
        private readonly SenderChannelConfiguration _configuration;
        protected IModel? _channel;
        public SenderChannel(IConnection connection, SenderChannelConfiguration configuration, IMessageSerializer<T> messageSerializer)
        {
            _channel = connection.CreateModel();
            _configuration = configuration;
            _messageSerializer = messageSerializer;
        }

        public void SendMessage(T message, string routingKey = "")
        {
            var serializedMessage = _messageSerializer.Serialize(message);
            {
                _channel.BasicPublish(_configuration.ExchangeName, routingKey, null, serializedMessage);
            }
        }
    }
}
