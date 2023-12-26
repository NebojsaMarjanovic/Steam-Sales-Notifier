using SteamSalesNotifier.Crawler.Models;
using System.Text.Json;

namespace SteamSalesNotifier.Crawler.Utilities.Serialization
{
    public class JsonSerializer<T>: IJsonSerializer<T> where T : class
    {
        public T? Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        public string Serialize(T value)
        {
            return JsonSerializer.Serialize(value);
        }
    }
}
