using System.Threading.Tasks;

namespace PF.WebAPI.Services.Filtering
{
    public interface IPrimeTester
    {
        int MaxValueSupported { get; }

        Task<bool> NumberIsPrime(double n);
    }
}