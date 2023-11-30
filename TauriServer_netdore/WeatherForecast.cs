namespace TauriServer_netdore
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }

    public class Update
    {
        public string version { get; set; }
        public DateTime pub_date { get; set; }
        public string url { get; set; }
        public string signature { get; set; }
        public string notes { get; set; }
    }
}