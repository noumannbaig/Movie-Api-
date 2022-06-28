using Backend.Application.Wrappers;
using Backend.Application.DataTransferObjects.Shared;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Newtonsoft.Json;
using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;

using Backend.Data;
using Backend.Application.Interfaces;
using Backend.Web.Services.Shared;

namespace Backend.Web.Extensions
{
    public static class AdminServicesConfig
    {
        public static IServiceCollection AddAdminServicesConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUtility, Utility>();
            return services;
        }
    }
}
