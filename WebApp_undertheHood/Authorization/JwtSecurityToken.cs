using Newtonsoft.Json;

namespace WebApp_undertheHood.Authorization
{
  public class JwtSecurityToken
  {
    [JsonProperty("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonProperty("expires_at")]
    public DateTime ExpiresAt { get; set; }
  }
}