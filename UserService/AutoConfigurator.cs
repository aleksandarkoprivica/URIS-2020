using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Helpers;

namespace UserService
{
    public static class AutoConfigurator
    {
        public static void SetupService(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
                    typeof(IConfigurable).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IConfigurable>().ToList();

            installers.ForEach(installer => installer.Setup(services, configuration));
        }
    }
}