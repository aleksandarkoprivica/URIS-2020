using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GatewayService.Helpers
{
    public interface IConfigurable
    {
        void Setup(IServiceCollection services, IConfiguration configuration);
    }
}