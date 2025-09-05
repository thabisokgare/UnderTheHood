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
        public HRManagerModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {
            var client = httpClientFactory.CreateClient("WebApi");
            // Call your API endpoints using the client
            client.GetFromJsonAsync<WeatherForecastDTO>("WeatherForecast");
         }
        
    }
}
