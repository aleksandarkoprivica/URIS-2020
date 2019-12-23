using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.DAL;
using UserService.Helpers;
using UserService.Services;
using UserService.Services.Impl;

namespace UserService.Configs
{
    public class DIConfig: IConfigurable
    {
        public void Setup(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, Services.UserService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IAssistentService, AssistentService>();
            services.AddScoped<IProfessorService, ProfessorService>();
            services.AddScoped<ICacheService, RedisService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserSessionService, UserSessionService>();
            services.AddScoped<UserOperations, UserOperations>();
            services.AddScoped<StudentDAO, StudentDAO>();
            services.AddScoped<AssistentDAO, AssistentDAO>();
            services.AddScoped<ProfessorDAO, ProfessorDAO>();
        }
    }
}