using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PF.WebAPI.Services.Filtering
{
    public class PrimesFilter : IWordFilter
    {
        private readonly ILogger<PrimesFilter> _logger;
        private readonly IPrimeTester _primeTester;

        public PrimesFilter(ILogger<PrimesFilter> logger, IPrimeTester primeTester)
        {
            _logger = logger;
            _primeTester = primeTester;
        }

        public async Task<List<string>> Filter(IEnumerable<string> words)
        {
            var tasks = words.Select(word => TestWord(word)).ToList();

            await Task.WhenAll(tasks.ToArray());

            return tasks.Where(t => t.Result.Value).Select(task => task.Result.Key).ToList();
        }

        private async Task<KeyValuePair<string, bool>> TestWord(string word)
        {
            bool addWord = true;
            if (double.TryParse(word, out var result))
            {
                if (result > _primeTester.MaxValueSupported)
                {
                    var message =
                        $"Number to be tested ({result}) exceeds the capability of the current prime number tester ({_primeTester.MaxValueSupported})";
                    _logger.LogError(message);
                    throw new NumberExceedsPrimeSearchBoundsException(message, result, _primeTester.MaxValueSupported);
                }

                addWord = !await _primeTester.NumberIsPrime(result);
            }

            return new KeyValuePair<string, bool>(word, addWord);
        }

        public string Name => "Remove Prime Number Filter";
    }
}