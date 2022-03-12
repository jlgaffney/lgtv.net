using LgTv.Clients;

namespace LgTv.Sample.Blazor.Services
{
    public class ProxyHostConfiguration
    {
        public ProxyHostConfiguration(
            HostConfiguration configuration)
        {
            Configuration = configuration;
        }

        public HostConfiguration Configuration { get; }
    }
}
