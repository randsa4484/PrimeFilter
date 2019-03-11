using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PF.WebAPI.ServiceRegistration
{
    public static class RegisterServices
    {
        public static IServiceCollection AddFromConfigurationFile(this IServiceCollection services,
            IConfigurationSection configuration)
        {
            var servicesConfiguration = configuration.Get<ServicesConfiguration>();

            if (servicesConfiguration.Singleton != null)
            {
                foreach (var service in servicesConfiguration.Singleton)
                {
                    services.AddSingleton(Type.GetType(service.Service), Type.GetType(service.Implementation));
                }
            }

            if (servicesConfiguration.Transient != null)
            {
                foreach (var service in servicesConfiguration.Transient)
                {
                    var sType = Type.GetType(service.Service);
                    var iType = Type.GetType(service.Implementation);
                    services.AddTransient(sType, iType);
                }
            }

            return services;
        }
    }
}