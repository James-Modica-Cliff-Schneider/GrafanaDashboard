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
    public class StackedBarChartController : Controller
    {
        private string tablename = "DataTimeSeries";

        private static readonly Dictionary<string, string> DataSeriesGenerators = new Dictionary<string, string>
        {
            { "WiserConnect", "WiserConnect" },
        };



        [Route("")]
        [HttpGet]
        public IActionResult Get() => Ok();

        [HttpPost("search")]
        public IActionResult Search()
        {
            return Ok(DataSeriesGenerators.Keys);
        }

        [HttpPost("query")]
        public async Task<IActionResult> Query([FromBody] QueryViewModel query)
         {
            if (!ModelState.IsValid)
            {
                
                return BadRequest(ModelState);
            }

            var date = DateTime.Now;
            var dataFrom = new DateTime(date.Year,1,1);
            var dataTo = date;

            var queryToPreform = query.Targets;

            ITimeSeriesData timeSeriesData = new GetData();

            RequestHandler handler = new RequestHandler(timeSeriesData);

            var outputSeries = await handler.GetOutputBarChart(dataTo, dataFrom, queryToPreform, tablename);


            return Ok(new[] {outputSeries});
        }

        [HttpPost("annotations")]
        public IActionResult GetAnnotations()
        {
            return Ok();
        }
    }
}
