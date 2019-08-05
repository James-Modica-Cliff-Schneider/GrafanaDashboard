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
    public class MostCommonRecentProblemController : Controller
    {
        private string tablename = "MostCommonRecentProblems";

        private static readonly Dictionary<string, string> DataSeriesGenerators = new Dictionary<string, string>
        {
            { "FindMostCommonRecentProblems","FindMostCommonRecentProblems" },


        };



        [Route("MostCommonRecentProblems")]
        [HttpGet]
        public IActionResult Get() => Ok();

        [HttpPost("MostCommonRecentProblems/search")]
        public IActionResult Search()
        {
            return Ok(DataSeriesGenerators.Keys);
        }

        [HttpPost("MostCommonRecentProblems/query")]
        public async Task<IActionResult> Query([FromBody] QueryViewModel query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            var queryToPreform = query.Targets;

            ITimeSeriesData timeSeriesData = new GetData();

            RequestHandler handler = new RequestHandler(timeSeriesData);

            var outputSeries = await handler.GetMostCommonRecentProblem(queryToPreform, tablename);


            return Ok(outputSeries);
        }

        [HttpPost("MostCommonRecentProblems/annotations")]
        public IActionResult GetAnnotations()
        {
            return Ok();
        }
    }
}
