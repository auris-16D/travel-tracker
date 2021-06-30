using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TravelTracker.Models;
//using MySqlConnector;

namespace TravelTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrekController : ControllerBase
    {
        private readonly ILogger<TrekController> logger;

        //private readonly MySqlConnection mySqlConnection;

        public TrekController(ILogger<TrekController> logger, IServiceProvider provider)
        {
            this.logger = logger;
            //this.mySqlConnection = mySqlConnection;
        }

        [HttpGet]
        public IEnumerable<TrekRequestModel> Get()
        {
            List<TrekRequestModel> treks = new List<TrekRequestModel>();
            using (var context = new TrekContext())
            {
                foreach(var trek in context.Treks.Include(p => p.Owner))
                {
                    treks.Add(
                        new TrekRequestModel
                        {
                            OwnerId = trek.Owner.Id.ToString(),
                            Area = trek.Area,
                            StartTime = DateTimeOffset.FromUnixTimeMilliseconds(trek.StartTime).LocalDateTime,
                            FinishTime = DateTimeOffset.FromUnixTimeMilliseconds(trek.FinishTime).LocalDateTime,
                            StartPoint = new TrekRequestModel.Coordinate
                            {
                                Lattitude = trek.StartPointLatitude,
                                Longitude  = trek.StartPointLongitude
                            },
                            FinishPoint = new TrekRequestModel.Coordinate
                            {
                                Lattitude = trek.FinishPointLatitude,
                                Longitude =  trek.FinishPointLongitude
                            },
                            Weather = trek.Weather,
                            Summary = trek.Summary,
                        }
                    );
                }
            }
            return treks;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Guid> Post(TrekRequestModel trekRequest)
        {

            Trek trek = new Trek();
            using (var context = new TrekContext())
            {
                trek = new Trek
                {
                    Id = Guid.NewGuid(),
                    StartTime = new DateTimeOffset(trekRequest.StartTime).ToUnixTimeMilliseconds(),
                    FinishTime = new DateTimeOffset(trekRequest.FinishTime).ToUnixTimeMilliseconds(),
                    Area = trekRequest.Area,
                    StartPointLatitude = trekRequest.StartPoint.Lattitude,
                    StartPointLongitude = trekRequest.StartPoint.Longitude,
                    FinishPointLatitude = trekRequest.FinishPoint.Lattitude,
                    FinishPointLongitude = trekRequest.FinishPoint.Longitude,
                    Weather = trekRequest.Weather,
                    Summary = trekRequest.Summary
                };
                trek.Owner = context.Owners.Find(Guid.Parse(trekRequest.OwnerId));
                context.Treks.Add(trek);
                var result = context.SaveChanges();
            }

            return trek.Id;
        }
    }
}
