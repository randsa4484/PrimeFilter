using System.Collections.Generic;

namespace PF.WebAPI.Services
{
    public interface IPrimesGenerator
    {
        IEnumerable<int> GeneratePrimes();
    }
}