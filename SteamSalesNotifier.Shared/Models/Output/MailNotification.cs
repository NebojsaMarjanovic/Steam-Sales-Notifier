using MemoryPack;
using SteamSalesNotifier.Shared.RabbitMq.Contracts;

namespace SteamSalesNotifier.Shared.Models.Output
{
    [MemoryPackable]
    public partial record MailNotification (List<User> Users, string Html) : IMessage;
}
