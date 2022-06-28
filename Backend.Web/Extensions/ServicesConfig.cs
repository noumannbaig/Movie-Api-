using Backend.Data.IRepositories;
using Backend.Data.IRepositories.ITest;
using Backend.Data.Repositories.TestRepository;
using Backend.Web.Modules.Test;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieAPI1.Interface;
namespace Backend.Web.Extensions
{
    public static class ServicesConfig
    {
        public static IServiceCollection AddServicesConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<MovieRepositoryInterface, RepositoryPatternClass>();

            //services.AddScoped<TestService, TestService>();

            return services;
        }
    }
}
