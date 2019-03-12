using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PF.WebAPI.Services.Filtering;
using PF.WebAPI.Services.Sorting;
using PF.WebAPI.Services.StringParsing;

namespace PF.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilterAndOrderController : ControllerBase
    {
        private readonly IStringParser _stringParser;
        private readonly IWordFilter _wordFilter;
        private readonly IWordSorter _wordSorter;

        public FilterAndOrderController(IStringParser stringParser, IWordFilter wordFilter, IWordSorter wordSorter)
        {
            _stringParser = stringParser;
            _wordFilter = wordFilter;
            _wordSorter = wordSorter;
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

            var wordList = (await _wordFilter.Filter(words)).ToList();

            wordList.Sort(_wordSorter.Compare);

            return new JsonResult(wordList);
        }
    }
}
