namespace WebApplication3;

public class WeatherDownloadService
{
    public async Task<string> GetWeatherData(string city)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://weather-by-api-ninjas.p.rapidapi.com/v1/weather?city={city}"),
            Headers =
            {
                { "X-RapidAPI-Key", "c405f28377msh312e734f0ac30abp101ffejsn7ff1533c10e2" },
                { "X-RapidAPI-Host", "weather-by-api-ninjas.p.rapidapi.com" },
            }
        };
        
        using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
    }
}