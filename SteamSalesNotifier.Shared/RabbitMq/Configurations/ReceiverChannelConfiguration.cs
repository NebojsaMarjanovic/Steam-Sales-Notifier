namespace SteamSalesNotifier.Shared.RabbitMq.Configurations
{
    public class ReceiverChannelConfiguration
    {
        public string ExchangeName { get; init; } = null!;
        public string QueueName { get; init; } = null!;
    }
}
