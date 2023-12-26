using SteamSalesNotifier.Crawler.Models;

namespace SteamSalesNotifier.Crawler.Contracts
{
    public interface ICrawler
    {
        Task<List<Game>> GetData(string url);
    }
}
