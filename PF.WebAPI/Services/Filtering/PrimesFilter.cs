using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<string>> Filter(IEnumerable<string> words)
        {
            var newWords = new List<string>();

            foreach (var word in words)
            {
                bool addWord = true;
                if (double.TryParse(word, out var result))
                {
                    if (result > _primeTester.MaxValueSupported)
                    {
                        var message = $"Number to be tested ({result}) exceeds the capability of the current prime number tester ({_primeTester.MaxValueSupported})";
                        _logger.LogError(message);
                        throw new NumberExceedsPrimeSearchBoundsException(message, result, _primeTester.MaxValueSupported);
                    }

                    addWord = ! await _primeTester.NumberIsPrime(result);
                }

                if (addWord)
                    newWords.Add(word);
            }

            return newWords;
        }

        public string Name => "Remove Prime Number Filter";
    }
}