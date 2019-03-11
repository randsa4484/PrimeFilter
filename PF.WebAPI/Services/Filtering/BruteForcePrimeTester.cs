using System;
using System.Linq;
using System.Threading.Tasks;

namespace PF.WebAPI.Services.Filtering
{
    public class BruteForcePrimeTester : IPrimeTester
    {
        private readonly IOrderedEnumerable<int> _primes;
  
        public BruteForcePrimeTester(IPrimesGenerator primesGenerator)
        {
            _primes = primesGenerator.Primes.OrderBy(x => x);

            // We are testing a number is prime by testing its divisibility
            // by all primes less than its square root.
            // So for instance, testing 35 is prime requires testing ints from 2 to 5
            // The corollary of this is that the largest number that can definitely be tested is
            // one less than the square of our largest test prime + 1

            var maxTestPrime = _primes.Max();
            MaxValueSupported = ((maxTestPrime + 1) * (maxTestPrime + 1)) - 1;
        }

        public int MaxValueSupported { get; }
        
        public async Task<bool> NumberIsPrime(double n)
        {
            throw new NotImplementedException();
            //await Task.Run(NumberIsPrimeImp(n));
        }

        public bool NumberIsPrimeImp(double n)
        {
            // treat -ve and +ive values equally 
            var absValue = Math.Abs(n);
            // test for non-zero mantissa
            if (absValue % 1 >= double.Epsilon) return false;

            // we now know we have an int
            var testVal = (int)absValue;

            // By convention, 0 and 1 are defined as not prime
            if(testVal == 0 || testVal == 1)
                return false;

            // start testing from lowest prime first
            // (i.e. all even numbers are ruled out on first iteration
            foreach (var prime in _primes)
            {
                if (testVal == prime)
                    return true;
                if (testVal % prime == 0)
                    return false;
            }

            // we've tested with all our primes.
            // Need to test that we haven't prematurely run out of primes to test with

            var sqrRoot = Math.Sqrt(testVal);

            // if number is square, it can't be prime
            if (sqrRoot % 1 < double.Epsilon) return false;

            // but if the value tested exceeds max supported,
            // we ran out of primes

            if (absValue > this.MaxValueSupported)
                throw new NotSupportedException("Value being tested exceeds the maximum currently supported");

            return true;
        }
    }
}