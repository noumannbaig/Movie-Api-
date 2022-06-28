using Backend.Application.Wrappers;
using Backend.Application.DataTransferObjects.Shared;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json;
using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;

using Backend.Data;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Backend.Web.Extensions
{
    public static class JwtTokenConfig
    {

        public static IServiceCollection AddJwtTokenAuthentication(this IServiceCollection services, IConfiguration configuration, byte[] key)
        {

            services.Configure<JwtConfig>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

             .AddJwtBearer("Fit", x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidIssuer = configuration["JWT:Issuer"],
                     ValidAudience = configuration["JWT:Audience"],
                 };
                 //x.Events.OnMessageReceived = context => {

                 //    if (context.Request.Cookies.ContainsKey("authToken"))
                 //    {
                 //        context.Token = context.Request.Cookies["authToken"];
                 //    }

                 //    return Task.CompletedTask;
                 //};

             });
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes("Fit")
                    .Build();

                options.AddPolicy("FitPolicy", policy =>
                {
                    policy.AddAuthenticationSchemes("Fit");
                    policy.RequireClaim("project_scope", "Fit");
                    //policy.RequireClaim(ClaimTypes.Role, _context.TblRoles.Where(x => x.ProjectId == 2).Select(x => x.Name).ToList());
                });
            });
            return services;
        }
    }
}
