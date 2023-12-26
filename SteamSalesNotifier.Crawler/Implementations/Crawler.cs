using Microsoft.Extensions.Logging;
using SteamSalesNotifier.Crawler.Contracts;
using SteamSalesNotifier.Crawler.Models;
using SteamSalesNotifier.Crawler.Utilities.Serialization;

namespace SteamSalesNotifier.Crawler.Implementations
{
    public class Crawler : ICrawler
    {
        private readonly HttpClient _httpClient;
        private readonly IJsonSerializer<Response> _jsonSerializer;
        private readonly ILogger<Crawler> _logger;

        public Crawler(IHttpClientFactory httpClientFactory, IJsonSerializer<Response> jsonSerializer, ILogger<Crawler> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _jsonSerializer = jsonSerializer;
            _logger = logger;
        }

        public async Task<List<Game>> GetData(string url)
        {
            List<Game>? data = new();
            _logger.LogInformation("Fetching data from Steam api...");

            try
            {
                var json = await _httpClient.GetStringAsync(url);

                data = _jsonSerializer?.Deserialize<Response>(json)?.Root.Games;

                if (data is null)
                    throw new Exception("Could not retrive data from Steam api.");

                _logger.LogInformation("Finished fetching data from Steam api.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return data;
        }
    }
}
