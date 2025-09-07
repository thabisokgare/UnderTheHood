using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp_undertheHood.DTO;
using Newtonsoft.Json;
using WebApp_undertheHood.Authorization;
using System.Net.Http.Headers;


namespace WebApp_undertheHood.Pages
{
    [Authorize(Policy = "HRManagerOnly")]
    public class HRManagerModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        public List<WeatherForecastDTO>? WeatherForecastsItems { get; set; }

        public HRManagerModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = httpClientFactory.CreateClient("WebApi");
            var response = await client.PostAsJsonAsync("Auth", new LoginRequest { Email = "admin", Password = "password" });
            response.EnsureSuccessStatusCode();
            string strJwt = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<JwtSecurityToken>(strJwt);
            // add token to  the Httpheader
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken ?? string.Empty);
            // Call your API endpoints using the client
            WeatherForecastsItems = await client.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast");
        }

    }
}
