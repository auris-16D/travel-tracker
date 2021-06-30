using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using Newtonsoft.Json;

namespace TravelTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrekController : ControllerBase
    {
        private readonly ILogger<TrekController> logger;

        private readonly MySqlConnection mySqlConnection;

        public TrekController(ILogger<TrekController> logger, IServiceProvider provider, MySqlConnection mySqlConnection)
        {
            this.logger = logger;
            this.mySqlConnection = mySqlConnection;
        }

        [HttpGet]
        public IEnumerable<Trek> Get()
        {
            List<Trek> treks = new List<Trek>();
            mySqlConnection.Open();

            string query = @"SELECT * FROM treks;";

            var command = new MySqlCommand(query, mySqlConnection);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var trek_date = (DateTime)reader.GetValue("trek_date");
                var area = (string)reader.GetValue("area");
                var duration = (int)reader.GetValue("duration");
                var weather = (string)reader.GetValue("weather");
                var summary = (string)reader.GetValue("summary");

                Trek trek = new Trek
                {
                    Area = area,
                    TrekDate = trek_date,
                    Duration = duration,
                    Summary = summary,
                    Weather = (Weather)Enum.Parse(typeof(Weather),weather)
                };
                treks.Add(trek);
            }

            mySqlConnection.Close();

            return treks;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Trek> Post(Trek trek)
        {
            mySqlConnection.Open();

            string query = $"INSERT INTO treks VALUES('{trek.TrekDate}', '{trek.Area}', {trek.Duration}, '{trek.Weather}', '{trek.Summary}');";

            var command = new MySqlCommand(query, mySqlConnection);
            command.ExecuteNonQuery();
            return trek;
        }
    }
}
