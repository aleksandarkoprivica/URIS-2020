using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Helpers;

namespace UserService.Configs
{
    public class WebContext: IConfigurable
    {
        public void Setup(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
        }
    }
}