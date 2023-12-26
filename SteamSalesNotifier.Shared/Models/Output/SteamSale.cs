using MemoryPack;
using SteamSalesNotifier.Shared.RabbitMq.Contracts;

namespace SteamSalesNotifier.Shared.Models.Output
{
    [MemoryPackable]
    public partial record SteamSale(List<Game> Games) : IMessage;
   
}
