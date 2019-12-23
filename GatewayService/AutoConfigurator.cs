using System;
using System.Linq;
using FluentValidation.Internal;
using GatewayService.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GatewayService
{
    public static class AutoConfigurator
    {
        public static void SetupService(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
                    typeof(IConfigurable<,>).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IConfigurable>().ToList();

            installers.ForEach(installer => installer.Setup(services, configuration));
        }
    }
}