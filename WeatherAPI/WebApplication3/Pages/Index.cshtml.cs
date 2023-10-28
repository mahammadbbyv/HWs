using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public Data? Weather { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            var service = new WeatherDownloadService();
            string? city = Request.Form["city"];
            string? json = service.GetWeatherData(city).Result;
            Weather = JsonSerializer.Deserialize<Data>(json);
        }
    }
}