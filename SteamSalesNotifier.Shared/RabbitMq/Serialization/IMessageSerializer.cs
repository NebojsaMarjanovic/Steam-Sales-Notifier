using SteamSalesNotifier.Shared.RabbitMq.Contracts;

namespace SteamSalesNotifier.Shared.RabbitMq.Serialization
{
    public interface IMessageSerializer<T> where T: class, IMessage
    {
        byte[] Serialize(T message);

        T? Deserialize<T>(byte[] message);
    }
}
