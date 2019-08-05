using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataRepository;
using Model;
using Model.ViewModels;

namespace SimpleJsonDataSource.Controllers
{
	
	public class TimeSeriesController : Controller
	{
        private string tablename = "DataTimeSeries";

        private static readonly Dictionary<string, string> DataSeriesGenerators = new Dictionary<string, string>
        {
            { "ConnectedDeviceCount", "ConnectedDeviceCount" },
            { "ControllerCount","ControllerCount" },
            { "HeatingActuatorCount","HeatingActuatorCount" },
            { "HeimanSmartPlugCount","HeimanSmartPlugCount" },
            { "ItrvsCount","ItrvsCount" },
            { "LoadActuatorCount","LoadActuatorCount" },
            { "RoomStatCount","RoomStatCount" },
            { "UnderFloorHeatingCount","UnderFloorHeatingCount" },
            { "WT724R1S0902Count","WT724R1S0902Count" },
            { "WT714R1S0902Count","WT714R1S0902Count" },
            { "WT704R1S1804Count","WT704R1S1804Count" },
            { "WT734R1S0902Count","WT734R1S0902Count" },
            { "WT704R1S30S2Count","WT704R1S30S2Count" },
            { "WT714R1S30S2Count","WT714R1S30S2Count" },
            { "WT704R1A580HCount","WT704R1A580HCount" },
            { "WT714R1A580HCount","WT714R1A580HCount" },
            { "WT704R1A30S4Count","WT704R1A30S4Count" },
            { "EcoModeUsage","EcoModeUsage" },
            { "ComfortModeUsage","ComfortModeUsage" },
            { "OpenWindowDetectionUsage","OpenWindowDetectionUsage" },


        };



        [Route("TimeSeries")]
        [HttpGet]
		public IActionResult Get() => Ok();

		[HttpPost("TimeSeries/search")]
		public IActionResult Search()
		{
			return Ok(DataSeriesGenerators.Keys);
		}

		[HttpPost("TimeSeries/query")]
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

           var outputSeries = await handler.GetOutputTimeSeries(dataTo, dataFrom, queryToPreform, tablename);


            return Ok(outputSeries);
        }

        [HttpPost("TimeSeries/annotations")]
		public IActionResult GetAnnotations()
		{
			return Ok();
		}
	}
}
