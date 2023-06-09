using System.Text.Json.Serialization;

namespace Application.Models.Token;

public class Tokens
{
    [JsonPropertyName("access_token")]
    public string Access_Token { get; set; }

    [JsonPropertyName("refresh_token")]
    public string Refresh_Token { get; set; }
}
