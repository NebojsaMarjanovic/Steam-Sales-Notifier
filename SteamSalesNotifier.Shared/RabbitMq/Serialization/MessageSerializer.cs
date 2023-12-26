using MemoryPack;
using SteamSalesNotifier.Shared.RabbitMq.Contracts;

namespace SteamSalesNotifier.Shared.RabbitMq.Serialization
{
    public class MessageSerializer<T> : IMessageSerializer<T> where T : class,IMessage
    {
        public T? Deserialize<T>(byte[] message)
        {
            return MemoryPackSerializer.Deserialize<T> (message);
        }

        public byte[] Serialize(T message)
        {
            return MemoryPackSerializer.Serialize(message);
        }
    }
}
