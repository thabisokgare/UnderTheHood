using Newtonsoft.Json;

namespace WebApp_undertheHood.Authorization;

public class LoginResponse
{
  [JsonProperty("access_token")]
    public string AccessToken { get; set; } = string.Empty;
    [JsonProperty("expires_at")]
    public string ExpiresAt { get; set; } = string.Empty;
}