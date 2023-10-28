using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication3;

public class Data
{
    [JsonPropertyName("cloud_pct")]
    public int Cloud_pct { get; set; }
    [JsonPropertyName("temp")]
    public int Temp { get; set; }
    [JsonPropertyName("feels_like")]
    public int Feels_like { get; set; }
    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }
    [JsonPropertyName("min_temp")]
    public int Min_temp { get; set; }
    [JsonPropertyName("max_temp")]
    public int Max_temp { get; set; }
    [JsonPropertyName("wind_speed")]
    public double Wind_speed { get; set; }
    [JsonPropertyName("wind_degrees")]
    public int Wind_degrees { get; set; }
    [JsonPropertyName("sunrise")]
    public int Sunrise { get; set; }
    [JsonPropertyName("sunset")]
    public int Sunset { get; set; }
}