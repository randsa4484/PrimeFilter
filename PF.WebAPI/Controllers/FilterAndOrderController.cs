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

        // GET api/values
        [HttpGet("SpaceDelimitedFilter")]
        public async Task<IActionResult> FilterAndOrder(string numbersString)
        {
            var words = _stringParser.ParseString(numbersString, " ");

            words = await _wordFilter.Filter(words);

            return new JsonResult(words.OrderByDescending(x => x));
        }
    }
}
