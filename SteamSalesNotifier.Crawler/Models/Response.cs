using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace SteamSalesNotifier.Crawler.Models
{
    public record Response(
        [property: JsonPropertyName("specials")] Root Root
        );

    public record Root(
        [property: JsonPropertyName("id"), JsonIgnore] string Id,
        [property: JsonPropertyName("name"), JsonIgnore] string Name,
        [property: JsonPropertyName("items")] List<Game> Games
        );

    public record Game(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("discounted")] bool IsDiscounted,
        [property: JsonPropertyName("discount_percent")] int DiscountPercent,
        [property: JsonPropertyName("original_price")] int OriginalPrice,
        [property: JsonPropertyName("final_price")] int FinalPrice,
        [property: JsonPropertyName("currency")] string Currency,
        [property: JsonPropertyName("windows_available")] bool WindowsAvailable,
        [property: JsonPropertyName("mac_available")] bool MacAvaliable,
        [property: JsonPropertyName("linux_available")] bool LinuxAvailable,
        [property: JsonPropertyName("header_image")] string Image);
}
