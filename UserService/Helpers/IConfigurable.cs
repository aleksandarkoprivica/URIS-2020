using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.Helpers
{
    public interface IConfigurable
    {
        void Setup(IServiceCollection services, IConfiguration configuration);
    }
}