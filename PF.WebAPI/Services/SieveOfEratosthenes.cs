using System;
using System.Collections.Generic;
using System.Linq;

namespace PF.WebAPI.Services
{
    public class SieveOfEratosthenes : IPrimesGenerator
    {
        private readonly int _limit;

        public SieveOfEratosthenes(IPrimeGeneratorInitialiser initialiser)
        {
            if(initialiser == null) throw new NullReferenceException();

            _limit = initialiser.Limit;
        }

        public IEnumerable<int> GeneratePrimes()
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