namespace SteamSalesNotifier.Crawler.Utilities.Serialization
{
    public interface IJsonSerializer<T> where T: class
    {
        string Serialize(T value);

        T? Deserialize<T>(string json);
    }
}
