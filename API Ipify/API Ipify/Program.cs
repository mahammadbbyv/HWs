using API_Ipify;
HttpClient client = new();
var res1 = Newtonsoft.Json.JsonConvert.DeserializeObject<IPLocation>(await client.GetStringAsync("https://api.ipify.org/?format=json"));
var res2 = Newtonsoft.Json.JsonConvert.DeserializeObject<MainData>(await client.GetStringAsync($"https://api.ipgeolocation.io/ipgeo?apiKey=5a8fcb10be3644aeb21fd41caa25fb57&ip={res1.IP}"));
Console.WriteLine($"Country: {res2.country_name}\nCity: {res2.country_capital}\nFlag: {res2.country_flag}\nCurrency: {res2.currency.symbol}");