using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PF.WebAPI.Services.Filtering;
using PF.WebAPI.Services.StringParsing;

namespace PF.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilterAndOrderController : ControllerBase
    {
        private readonly IStringParser _stringParser;
        private readonly IWordFilter _wordFilter;

        public FilterAndOrderController(IStringParser stringParser, IWordFilter wordFilter)
        {
            _stringParser = stringParser;
            _wordFilter = wordFilter;
        }

        [HttpGet("SpaceDelimited")]
        public async Task<IActionResult> FilterAndOrder(string stringToTest)
        {
            return await FilterAndOrder(stringToTest, " ");
        }

        [HttpGet("UserDefinedDelimiter")]
        public async Task<IActionResult> FilterAndOrder(string stringToTest, string delimiter)
        {
            var words = _stringParser.ParseString(stringToTest, delimiter);

            words = await _wordFilter.Filter(words);

            return new JsonResult(words.OrderByDescending(x => x));
        }
    }
}
