namespace SteamSalesNotifier.Shared.RabbitMq.Contracts
{
    public interface IReceiverChannel<T>
    {
        void StartReceiving();
        Task ProcessMessage(T message);
    }
}
