
namespace SteamSalesNotifier.Shared.RabbitMq.Contracts
{
    public interface ISenderChannel<T>
    {
        void SendMessage(T message, string routingKey="");
    }
}
