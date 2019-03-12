using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PF.WebAPI.Services.Filtering
{
    public interface IPrimeGeneratorInitialiser
    {
        int Limit { get; }
    }

    public class PrimeGeneratorConfigurationInitialiser : IPrimeGeneratorInitialiser
    {
        public PrimeGeneratorConfigurationInitialiser(ILogger<PrimeGeneratorConfigurationInitialiser> logger, IConfiguration configuration)
        {
            var limit = configuration["PrimesTesting:LimitForPrimeGeneration"];

            if (!int.TryParse(limit, out var lim))
                throw new Exception("PrimesTesting:LimitForPrimeGeneration needs to be set");
            
            Limit = lim;
        }

        public int Limit { get; }
    }
}