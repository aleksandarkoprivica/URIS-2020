using System;
using EasyCaching.Core.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Helpers;

namespace UserService.Configs
{
    public class RedisConfig: IConfigurable
    {
        public void Setup(IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var redisHost = appSettings.RedisHost;
            var redisPort = appSettings.RedisPort;
            
            services.AddEasyCaching(options =>
            {
                options.UseRedis(redisConfig =>
                    {
                        redisConfig.DBConfig.Endpoints.Add(new ServerEndPoint(redisHost, Convert.ToInt32(redisPort)));
                        redisConfig.DBConfig.AllowAdmin = true;
                    },
                    "redis1");
            });
        }
    }
}