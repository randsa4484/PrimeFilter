using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using PF.WebAPI.Services.Filtering;

namespace PF.WebAPI.Controllers
{
    [Route("[controller]")]
    public class HealthController : Controller
    {
        private readonly IPrimeTester _primeTester;
        private readonly IWordFilter _wordFilter;

        public HealthController(IPrimeTester primeTester, IWordFilter wordFilter)
        {
            _primeTester = primeTester;
            _wordFilter = wordFilter;
        }

        [HttpGet]
        public dynamic Get()
        {
            var WordFilter = $"Word filter in use is {_wordFilter.Name}";
            var MaxSupportedNumber = _primeTester.MaxValueSupported;

            var ApplicationVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();

            return new
            {
                WordFilter,
                MaxSupportedNumber,
                ApplicationVersion
            };
        }

    }
}