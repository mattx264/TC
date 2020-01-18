using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;
using System.Threading.Tasks;
using TC.DataAccess;
using TC.DataAccess.DatabaseContext;
using TC.DataAccess.Repositories;
using TC.Entity.Entities;
using TC.WebService.Helpers;
using TC.WebService.Hubs;
using TC.WebService.Services;

namespace TC.WebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDefaultIdentity<UserModel>()
                .AddEntityFrameworkStores<TestingCenterDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                // Identity made Cookie authentication the default.
                // However, we want JWT Bearer Auth to be the default.
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    // Configure JWT Bearer Auth to expect our security key
                    options.TokenValidationParameters =
                       new TokenValidationParameters
                       {
                           LifetimeValidator = (before, expires, token, param) =>
                           {
                               return expires > DateTime.UtcNow;
                           },
                           ValidateAudience = false,
                           ValidateIssuer = false,
                           ValidateActor = false,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,
                           ValidIssuer = Configuration["Jwt:Issuer"],
                           ValidAudience = Configuration["Jwt:Issuer"],
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                       };

                    // We have to hook the OnMessageReceived event in order to
                    // allow the JWT authentication handler to read the access
                    // token from the query string when a WebSocket or 
                    // Server-Sent Events request comes in.
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/hubs")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                                context.HttpContext.Request.Headers["Authorization"] = accessToken;
                                // context.HttpCon = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                }
                );

            services.AddDbContext<TestingCenterDbContext>(options =>
             options
             .UseLazyLoadingProxies()
             .UseSqlServer(
                 Configuration.GetConnectionString("DefaultConnection")));


            services.AddMvc();

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.WithOrigins("http://localhost:53353")
                           .WithOrigins("http://localhost:4200")
                           .WithOrigins("http://localhost:5000")
                           .WithOrigins("http://localhost:5200")
                           .WithOrigins("chrome-extension://kiaoamdhbhjfgjjfodecilhhohjpabni")
                           .AllowAnyMethod()
                           .AllowAnyHeader()                           
                           .AllowCredentials();
                }));

            services.AddSignalR(hubOptions=> {
                hubOptions.EnableDetailedErrors = true;
            });
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "localhost";

            });
            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILoggerManager, LoggerManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserHelper, UserHelper>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<TestInfoRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.ConfigureExceptionHandler(logger);

            app.UseRouting();

            app.UseAuthentication();
            // app.UseIdentityServer();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors("CorsPolicy");
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseEndpoints((endpoints) =>
            {
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapHub<SzwagierHub>("/hubs/szwagier");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            //app.UseSpa(spa =>
            //{
            //    // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //    // see https://go.microsoft.com/fwlink/?linkid=864501

            //    spa.Options.SourcePath = "ClientApp";

            //    if (env.IsDevelopment())
            //    {
            //        spa.UseAngularCliServer(npmScript: "start");
            //    }
            //});
            //app.UseMvc();
        }
    }
}