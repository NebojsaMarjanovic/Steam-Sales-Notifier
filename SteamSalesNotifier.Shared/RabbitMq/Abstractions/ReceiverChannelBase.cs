using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SteamSalesNotifier.Shared.RabbitMq.Configurations;
using SteamSalesNotifier.Shared.RabbitMq.Contracts;
using SteamSalesNotifier.Shared.RabbitMq.Serialization;

namespace SteamSalesNotifier.Shared.RabbitMq.Abstractions
{
    public abstract class ReceiverChannelBase<T> : IReceiverChannel<T> where T : class, IMessage
    {
        private readonly IConnection _connection;
        private readonly ReceiverChannelConfiguration _configuration;
        private readonly IMessageSerializer<T> _messageSerializer;
        protected IModel? _channel;

        protected ReceiverChannelBase(IConnection connection, ReceiverChannelConfiguration configuration, IMessageSerializer<T> messageSerializer)
        {
            _connection = connection;
            _configuration = configuration;
            _messageSerializer = messageSerializer;
        }

        public void StartReceiving()
        {
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclarePassive(_configuration.ExchangeName);
            _channel.QueueDeclarePassive(_configuration.QueueName);
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                T? message = _messageSerializer.Deserialize<T>(body);

                await ProcessMessage(message);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            string consumerTag = _channel.BasicConsume(_configuration.QueueName, false, consumer);
        }

        public abstract Task ProcessMessage(T message);
    }
}
