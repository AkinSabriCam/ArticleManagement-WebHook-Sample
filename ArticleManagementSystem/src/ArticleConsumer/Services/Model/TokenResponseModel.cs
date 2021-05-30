using System.Text.Json.Serialization;

namespace ArticleConsumer.Services.Model
{
    public class TokenResponseModel
    {
        [JsonPropertyName("token")] public string Token { get; set; }
        [JsonPropertyName("refreshToken")] public string RefreshToken { get; set; }
        [JsonPropertyName("expiresIn")] public long ExpiresIn { get; set; }
    }
}