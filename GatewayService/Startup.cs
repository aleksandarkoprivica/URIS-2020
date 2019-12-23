using System;
using System.Text;
using System.Threading.Tasks;
using EasyCaching.Core.Configurations;
using GatewayService.Helpers;
using GatewayService.Services;
using GatewayService.Services.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace GatewayService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddOcelot();

            services.AddScoped<ICacheService, RedisService>();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var redisHost = appSettings.RedisHost;
            var redisPort = appSettings.RedisPort;
            
            services.AddControllers();

            services.AddEasyCaching(options =>
            {
                options.UseRedis(redisConfig =>
                    {
                        redisConfig.DBConfig.Endpoints.Add(new ServerEndPoint(redisHost, Convert.ToInt32(redisPort)));
                        redisConfig.DBConfig.AllowAdmin = true;
                    },
                    "redis1");
            });
            
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer("EduSecurity", options=> {
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            // TODO: connect to redis and check userId
//                            var userId = int.Parse(context.Principal.Identity.Name);
                            var cacheService = context.HttpContext.RequestServices.GetService<ICacheService>();
                            cacheService.SetValue("Test", "Redis Works");
                            // TODO: Add custom logic
                            if (false)
                            {
                                
                                context.Fail("Unauthorized");
                            }

                            return Task.CompletedTask;
                        }
                    };

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            
            app.UseOcelot().Wait(); 
        }
    }
}