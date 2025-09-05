namespace WebApp_undertheHood.DTO
{
    public class WeatherForecastDTO
    {
        public DateTime Date { get; set; }
        public required string Summary { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
    }
}
