using MemoryPack;

namespace SteamSalesNotifier.Shared.Models
{
    [MemoryPackable]
    public partial record User(string Name, string Mail);
}
