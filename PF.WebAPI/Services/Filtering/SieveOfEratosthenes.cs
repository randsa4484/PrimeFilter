using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace PF.WebAPI.Services.Filtering
{
    public class SieveOfEratosthenes : IPrimesGenerator
    {
        private readonly ILogger<SieveOfEratosthenes> _logger;
        private readonly int _limit;
        private IEnumerable<int> _primes;

        public SieveOfEratosthenes(ILogger<SieveOfEratosthenes> logger, IPrimeGeneratorInitialiser initialiser)
        {
            if(initialiser == null) throw new NullReferenceException();
            _logger = logger;

            _limit = initialiser.Limit;
        }

        public void Initialise()
        {
            _primes = null;

            var s = new Stopwatch();
            s.Start();
            var primes = GeneratePrimes().ToList();
            s.Stop();
            if(primes.Any())
                _logger.LogInformation($"Sieve of Eratosthenes generated {primes.Count} primes ranging from {primes.Min()} to {primes.Max()} in {s.Elapsed.TotalSeconds} seconds");
            else
                _logger.LogError("Sieve of Eratosthenes generated no primes");
            _primes = primes;
        }

        public IEnumerable<int> Primes
        {
            get
            {
                if (_primes == null)
                {
                    Initialise();
                }
                return _primes;
            }
        }

        private IEnumerable<int> GeneratePrimes()
        {
            if(_limit < 2)
                return new List<int>();

            //Input: an integer n > 1.
            //  Let A be an array of Boolean values, indexed by integers 2 to n,
            // initially all set to true.
            // for i = 2, 3, 4, ..., not exceeding √n:
            //  if A[i] is true:
            //     for j = i2, i2+i, i2+2i, i2+3i, ..., not exceeding n:
            //       A[j] := false.
            // Output: all i such that A[i] is true.

            var dictionary = new Dictionary<int, bool>();

            for(var i = 2; i <= _limit; i++)
                dictionary.Add(i, true);

            var maxRootVal = (int)Math.Sqrt(_limit);

            for(var i = 2; i <= maxRootVal; i++)
            {
                if(dictionary[i] == true)
                {
                    var j = i*i;
                    while(j <= _limit)
                    {
                        dictionary[j] = false;
                        j += i;
                    }
                }
            }
   
            return dictionary.Where(d => d.Value).Select(d => d.Key).ToList();
        }

   
    }
}