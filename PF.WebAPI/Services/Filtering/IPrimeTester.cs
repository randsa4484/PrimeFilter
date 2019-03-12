using System;
using System.Threading.Tasks;

namespace PF.WebAPI.Services.Filtering
{
    public interface IPrimeTester
    {
        int MaxValueSupported { get; }

        Task<bool> NumberIsPrime(double n);
    }

    /// <summary>
    /// thrown by NumberIsPrime in event that number being prime cannot be
    /// evaluated as it exceeds the limits
    /// If this happens too often, need to consider using something such as Miller-Rabin primality test
    /// </summary>
    public class NumberExceedsPrimeSearchBoundsException : NotSupportedException
    {
        public double Number { get; }
        public double Limit { get; }

        public NumberExceedsPrimeSearchBoundsException(string msg, double number, double limit) : base(msg)
        {
            Number = number;
            Limit = limit;
        }
    }
}