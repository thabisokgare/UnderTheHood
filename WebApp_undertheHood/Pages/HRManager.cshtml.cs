using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp_undertheHood.DTO;

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
            // Call your API endpoints using the client
            WeatherForecastsItems = await client.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast");
        }

    }
}
