using System.Collections.Generic;
using System.Threading.Tasks;
using DataRepository;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.ViewModels;

namespace SimpleJsonDataSource.Controllers
{
    public class SeriesController : Controller
    {
        private string tablename = "DataTimeSeries";

        private static readonly Dictionary<string, string> DataSeriesGenerators = new Dictionary<string, string>
        {
            { "ITRVPopulationCount","ITRVPopulationCount" },
            { "HeimanSmartPlugPopulationCount","HeimanSmartPlugPopulationCount" },
            { "RoomStatPopulationCount","RoomStatPopulationCount" },
            { "UnderFloorHeatingPopulationCount","UnderFloorHeatingPopulationCount" },
            { "AllDevicesPopulationCount","AllDevicesPopulationCount" },

        };



        [Route("Series")]
        [HttpGet]
        public IActionResult Get() => Ok();

        [HttpPost("Series/search")]
        public IActionResult Search()
        {
            return Ok(DataSeriesGenerators.Keys);
        }

        [HttpPost("Series/query")]
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

            var outputSeries = await handler.GetOutputSeries(dataTo, dataFrom, queryToPreform, tablename);


            return Ok(outputSeries);
        }

        [HttpPost("Series/annotations")]
        public IActionResult GetAnnotations()
        {
            return Ok();
        }
    }
}
