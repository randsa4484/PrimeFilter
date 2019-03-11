using System.Collections.Generic;

namespace PF.WebAPI.Services.Filtering
{
    public interface IPrimesGenerator
    {
        void Initialise();
        IEnumerable<int> Primes { get; }
    }
}