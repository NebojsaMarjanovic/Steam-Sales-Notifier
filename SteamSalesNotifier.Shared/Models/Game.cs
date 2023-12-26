using MemoryPack;

namespace SteamSalesNotifier.Shared.Models
{
    [MemoryPackable]
    public partial class Game
    {
        public string Name { get; set; }
        public bool Discounted { get; set; }
        public int DiscountPercent { get; set;}
        public double OriginalPrice { get; set; }
        public double FinalPrice { get; set; }
        public string Currency { get; set; }
        public bool WindowsAvailable { get; set; }
        public bool MacAvailable { get; set; }
        public bool LinuxAvailable { get; set; }
        public string Image { get; set; }
    }
}
