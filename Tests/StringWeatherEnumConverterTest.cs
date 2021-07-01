namespace Tests
{
    using System.Collections.Generic;
    using System.Text.Json;
    using NUnit.Framework;
    using TravelTracker;

    public class StringWeatherEnumConverterTest
    {
        [Test]
        public void DeserializeErrorCodeTest()
        {
            Dictionary<string, Weather> expectations = GetWeatherCodeMapping();

            foreach (var kv in expectations)
            {
                var jsonString = string.Format("{{ \"weather\" : \"{0}\",  \"trekDate\": \"2021 - 06 - 29T19: 38:53.896Z\",\"area\": \"Skye\",\"duration\": 120,\"summary\": \"Great walk with great friends\" }}", kv.Key);
                var deserializedObject = JsonSerializer.Deserialize<TrekRequestModel>(jsonString);



                Assert.AreEqual(kv.Value, deserializedObject.Weather);
                Assert.AreEqual("Skye", deserializedObject.Area);
            }
        }

        private Dictionary<string, Weather> GetWeatherCodeMapping()
        {
            return new Dictionary<string, Weather>()
            {
                { "Sunny", Weather.Sunny},
                { "Bright", Weather.Bright}
            };
        }
    }
}
