using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using PF.WebAPI.Services.Filtering;
using PF.WebAPI.Services.Sorting;

namespace PF.WebAPI.Controllers
{
    [Route("[controller]")]
    public class HealthController : Controller
    {
        private readonly IPrimeTester _primeTester;
        private readonly IWordFilter _wordFilter;
        private readonly IWordSorter _wordSorter;

        public HealthController(IPrimeTester primeTester, IWordFilter wordFilter, IWordSorter wordSorter)
        {
            _primeTester = primeTester;
            _wordFilter = wordFilter;
            _wordSorter = wordSorter;
        }

        [HttpGet]
        public dynamic Get()
        {
            var WordFilter = $"Word filter in use is {_wordFilter.Name}";
            var MaxSupportedNumber = _primeTester.MaxValueSupported;
            var SortCriteria = _wordSorter.Description;

            var ApplicationVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();

            return new
            {
                WordFilter,
                MaxSupportedNumber,
                SortCriteria,
                ApplicationVersion
            };
        }

    }
}