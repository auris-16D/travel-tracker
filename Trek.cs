namespace TravelTracker
{
    using System;
    using System.Text.Json.Serialization;

    public enum Weather
    {
        Sunny,
        Bright,
        Overcast,
        LightRain,
        Rain,
        HeavyRain,
        Foggy,
        Misty,
        Snow,
        Gales,
        Windy,
        Hailstones,
        Sleet,
        Stormy
    }

    public class Trek
    {
        public DateTime TrekDate { get; set; }

        [JsonPropertyName("area")]
        public string Area { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("weather")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Weather Weather { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }
    }
}
