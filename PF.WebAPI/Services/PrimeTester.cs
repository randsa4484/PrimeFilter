using System;
using System.Collections.Generic;

namespace PF.WebAPI.Services
{
    public interface IPrimesCollection
    {
        IEnumerable<int> Primes {get;}
    }

    public class PrimeTester
    {
        private readonly IPrimesCollection _primesCollection;

        public PrimeTester(IPrimesCollection primesCollection)
        {
            _primesCollection = primesCollection;
        }
        
        public bool NumberIsPrime(double n)
        {
            if(Math.Abs(n) % 1 > Double.Epsilon) return false;

            var testVal = (int)n;

            if(testVal == 0 || testVal == 1)
                return false;

            throw new NotImplementedException();
        }
    }
}