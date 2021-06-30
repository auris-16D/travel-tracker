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

    public class TrekRequestModel
    {
        [JsonPropertyName("ownerId")]
        public string OwnerId { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("finishTime")]
        public DateTime FinishTime { get; set; }

        [JsonPropertyName("area")]
        public string Area { get; set; }

        [JsonPropertyName("weather")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Weather Weather { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("startPoint")]
        public Coordinate StartPoint { get; set; }

        [JsonPropertyName("finishPoint")]
        public Coordinate FinishPoint { get; set; }

        public class Coordinate
        {
            [JsonPropertyName("lattitude")]
            public double Lattitude { get; set; }

            [JsonPropertyName("longitude")]
            public double Longitude { get; set; }
        }
    }
}
