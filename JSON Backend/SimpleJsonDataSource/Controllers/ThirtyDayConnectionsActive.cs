using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataRepository;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.ViewModels;

namespace SimpleJsonDataSource.Controllers
{
    public class ThirtyDayConnectionsActive : Controller
    {
        private string tablename = "DataTimeSeries";

        private static readonly Dictionary<string, string> DataSeriesGenerators = new Dictionary<string, string>
        {
            { "ThirtyDayActiveConnections", "ThirtyDayActiveConnections" },
            
        };



        [Route("ThirtyDay")]
        [HttpGet]
        public IActionResult Get() => Ok();

        [HttpPost("ThirtyDay/search")]
        public IActionResult Search()
        {
            return Ok(DataSeriesGenerators.Keys);
        }

        [HttpPost("ThirtyDay/query")]
        public async Task<IActionResult> Query([FromBody] QueryViewModel query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var dataFrom = DateTime.Now.AddDays(-7);
            var dataTo = DateTime.Now;

            var queryToPreform = query.Targets;

            ITimeSeriesData timeSeriesData = new GetData();

            RequestHandler handler = new RequestHandler(timeSeriesData);

            var outputSeries = await handler.GetThirtyDayOutputSeries(dataTo, dataFrom, queryToPreform, tablename);


            return Ok(new[] { outputSeries });
        }

        [HttpPost("ThirtyDay/annotations")]
        public IActionResult GetAnnotations()
        {
            return Ok();
        }
    }

}

