
using SteamSalesNotifier.Shared.Models;

namespace SteamSalesNotifier.Formatter.Contracts
{
    public interface IFormatterService
    {
        Task<string?> FormatTemplate(string templateFilePath, List<Game> model);
    }
}
