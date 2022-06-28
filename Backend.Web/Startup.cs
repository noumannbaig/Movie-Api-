using Microsoft.AspNetCore.Authentication.JwtBearer;
using Backend.Application;
using Backend.Application.DataTransferObjects.Shared;
//using Microsoft.IdentityModel.Tokens;
//using Quartz;
using Backend.Data;
using Backend.Web.Extensions;
using Backend.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieAPI1.Models;
using MovieAPI1.Interface;
using Backend.Data.Repositories;
using Backend.Data.IRepositories;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json;
using Backend.Application.IdentityAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
namespace Backend.Web
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddApplicationLayer();
            services.AddScoped<MovieRepositoryInterface, RepositoryPatternClass>();
            services.AddScoped<TicketInterface, TicketRepository>();
            services.AddScoped<AuthInterface, JwtRepository>();
            services.AddControllers();
            services.AddApplicationLayer();
            DataBaseSettings.ConnectionString = Configuration["DataBaseSettings:ConnectionString"];
            //AWSS3Model.AccessKey = Configuration["AWSS3:AccessKey"];
            //AWSS3Model.SecretKey = Configuration["AWSS3:SecretKey"];
            services.AddApplicationLayer();
            services.AddIdentity<ApplicationUser, IdentityRole>()
         .AddEntityFrameworkStores<MovieAPI1Context>()
           .AddDefaultTokenProviders();
//services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieAPI1", Version = "v1" });
//});
            services.AddCors();
            // For Entity Framework  

            services.AddDbContext<MovieAPI1Context>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MovieAPI1Context")));

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            //var key = System.Text.Encoding.ASCII.GetBytes(appSettings.Secret);
            var key = System.Text.Encoding.ASCII.GetBytes(Configuration["JWT:Key"]);


            //services.AddControllersWithViews(options =>
            //    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            //services.AddAntiforgery(options =>
            //{
            //    //options.FormFieldName = "MyAntiForgeryField";
            //    //options.HeaderName = "MyAntiForgeryHeader";
            //    options.Cookie.Name = "authToken";
            //});
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(o =>
            //{
            //    var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
            //    o.SaveToken = true;
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = Configuration["JWT:Issuer"],
            //        ValidAudience = Configuration["JWT:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Key)
            //    };
            //});
            services.AddJwtTokenAuthentication(Configuration, key);
            services.AddServicesConfig(Configuration);
            services.AddAdminServicesConfig(Configuration);
            services.AddControllers(options => options.EnableEndpointRouting = false)
                    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddApiVersioningExtension();
            services.AddSwaggerConfiguration();
            services.AddHealthChecks();
            services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

            // If using Kestrel:  
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:  
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // migrate any database changes on startup (includes initial db creation)
            //dbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity.Web v1"));
            }

            //app.UseHttpsRedirection();
            app.UseSwaggerSetup();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });
            app.UseCors(MyAllowSpecificOrigins);
            app.UseCors(x => x
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 //  .AllowCredentials() // allow credentials
                 );

           
            app.UseMiddleware<AuthorizationMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Microsoft.OpenApi.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using MovieAPI1.Models;
//using MovieAPI1.Interface;
//using Backend.Data;
//using Backend.Data.Repositories;
//using Backend.Data.IRepositories;
//using System.Text.Json.Serialization;
//using Microsoft.AspNetCore.Mvc.Formatters;
//using System.Text.Json;
//using Backend.Application.IdentityAuth;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//namespace MovieAPI1
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {

//            services.AddScoped<MovieRepositoryInterface, RepositoryPatternClass>();
//            services.AddScoped<TicketInterface, TicketRepository>();
//            //services.AddTransient<BussinessLogic, DummyService>();
//            services.AddControllers();
//            services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieAPI1", Version = "v1" });
//            });
//            //services.AddControllers().AddJsonOptions(x =>
//            //    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//            services.AddControllers(options =>
//            {
//                options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
//                options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.Web)
//                {
//                    ReferenceHandler = ReferenceHandler.Preserve,
//                }));
//            });
//            services.AddDbContext<MovieAPI1Context>(options =>
//                    options.UseSqlServer(Configuration.GetConnectionString("MovieAPI1Context")));


//            services.AddIdentity<ApplicationUser, IdentityRole>()
//          .AddEntityFrameworkStores<MovieAPI1Context>()
//           .AddDefaultTokenProviders();

//            services.AddAuthentication(x =>
//            {
//                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            }).AddJwtBearer(o =>
//            {
//                var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
//                o.SaveToken = true;
//                o.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuer = false,
//                    ValidateAudience = false,
//                    ValidateLifetime = true,
//                    ValidateIssuerSigningKey = true,
//                    ValidIssuer = Configuration["JWT:Issuer"],
//                    ValidAudience = Configuration["JWT:Audience"],
//                    IssuerSigningKey = new SymmetricSecurityKey(Key)
//                };
//            });
////            services.AddSwaggerGen(swagger =>
////            {
////                //This is to generate the Default UI of Swagger Documentation
////                swagger.SwaggerDoc("v2", new OpenApiInfo
////                {
////                    Version = "v1",
////                    Title = "ASP.NET 5 Web API",
////                    Description = "Authentication and Authorization in ASP.NET 5 with JWT and Swagger"
////                });
////                // To Enable authorization using Swagger (JWT)
////                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
////                {
////                    Name = "Authorization",
////                    Type = SecuritySchemeType.ApiKey,
////                    Scheme = "Bearer",
////                    BearerFormat = "JWT",
////                    In = ParameterLocation.Header,
////                    Description = "Enter ‘Bearer’ [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
////                });
////                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
////            {
////               {
////                       new OpenApiSecurityScheme
////                 {
////                          Reference = new OpenApiReference
////                            {
////                                Type = ReferenceType.SecurityScheme,
////                                Id = "Bearer"
////                             }
////                 },
////                          new string[] {}

////}
////});
////            });
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//                app.UseSwagger();
//                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieAPI1 v1"));
//            }

//            app.UseHttpsRedirection();

//            app.UseRouting();

//            //app.UseAuthentication();

//            app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllers();
//            });
//        }
//    }
//}
