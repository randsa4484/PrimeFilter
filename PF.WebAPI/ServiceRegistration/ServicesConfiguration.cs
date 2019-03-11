using System.Collections.Generic;

namespace PF.WebAPI.ServiceRegistration
{
    public class ServicesConfiguration
    {
        public IEnumerable<ServiceItem> Singleton { get; set; }
        public IEnumerable<ServiceItem> Transient { get; set; }
    }
}