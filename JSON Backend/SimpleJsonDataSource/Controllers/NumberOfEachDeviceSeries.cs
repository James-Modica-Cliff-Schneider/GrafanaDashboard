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
    public class NumberOfEachDeviceSeries : Controller
    {
        private string tableName = "DataTimeSeries";

        private static readonly Dictionary<string, string> DataSeriesGenerators = new Dictionary<string, string>
        {

            { "NumberOfEachDeviceSeries","NumberOfEachDeviceSeries" },


        };



        [Route("NumberOfEachDeviceSeries")]
        [HttpGet]
        public IActionResult Get() => Ok();

        [HttpPost("NumberOfEachDeviceSeries/search")]
        public IActionResult Search()
        {
            return Ok(DataSeriesGenerators.Keys);
        }

        [HttpPost("NumberOfEachDeviceSeries/query")]
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

            var outputTimeSeries = await handler.GetNumberOfEachDeviceSeries(dataTo, dataFrom, queryToPreform, tableName);


            return Ok(outputTimeSeries);
        }

        [HttpPost("NumberOfEachDeviceSeries/annotations")]
        public IActionResult GetAnnotations()
        {
            return Ok();
        }

    }
}
