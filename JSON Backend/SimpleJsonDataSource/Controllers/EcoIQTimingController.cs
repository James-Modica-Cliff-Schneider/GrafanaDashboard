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
    public class EcoIQController : Controller
    {
        private string tableName = "EcoIQTiming";

        private static readonly Dictionary<string, string> DataSeriesGenerators = new Dictionary<string, string>
        {

            { "EcoIQTiming","EcoIQTiming" },
            

        };



        [Route("EcoIQTiming")]
        [HttpGet]
        public IActionResult Get() => Ok();

        [HttpPost("EcoIQTiming/search")]
        public IActionResult Search()
        {
            return Ok(DataSeriesGenerators.Keys);
        }

        [HttpPost("EcoIQTiming/query")]
        public async Task<IActionResult> Query([FromBody] QueryViewModel query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataFrom = query.Range.From;
            var dataTo = query.Range.To;


            var queryToPreform = query.Targets;

            ITimeSeriesData timeSeriesData = new GetData();

            RequestHandler handler = new RequestHandler(timeSeriesData);

            var outputTimeSeries = await handler.GetOutputEcoIQTiming(dataTo, dataFrom, queryToPreform,tableName);


            return Ok(outputTimeSeries);
        }

        [HttpPost("EcoIQTiming/annotations")]
        public IActionResult GetAnnotations()
        {
            return Ok();
        }
    }
}
