
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace TC.BrowserEngine.AdminPanel
{
    public class Startup
    {
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            //Configuration = configuration;
        }

        //public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //    //services.Configure<CookiePolicyOptions>(options =>
            //    //{
            //    //    options.CheckConsentNeeded = context => true;
            //    //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //    //});

            services.AddMvcCore(option => option.EnableEndpointRouting = false).AddNewtonsoftJson();
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:5200").AllowAnyHeader().AllowAnyOrigin();
                });
            });

            //    //services.AddSingleton<IUserIdProvider, NameUserIdProvider>();

            //    //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //    //services.AddScoped<ILoggerManager, LoggerManager>();
            //    //services.AddScoped<UserRepository>();
            //    //services.AddScoped<ProjectRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            app.UseFileServer();
            //    if (env.IsDevelopment())
            //    {
            app.UseCors(MyAllowSpecificOrigins);
            app.UseDeveloperExceptionPage();
            //    }
            //    else
            //    {
            //        app.UseExceptionHandler("/Error");
            //        app.UseHsts();
            //    }
            //    app.ConfigureExceptionHandler(logger);

            // app.UseRouting();

            //    app.UseAuthentication();
            //    // app.UseIdentityServer();
            //    app.UseAuthorization();

            //    app.UseHttpsRedirection();
            //    app.UseStaticFiles();
            //    app.UseCookiePolicy();

            //    app.UseEndpoints(endpoints =>
            //    {
            //        endpoints.MapControllerRoute(
            //            name: "default",
            //            pattern: "{controller}/{action=Index}/{id?}");
            //        endpoints.MapRazorPages();
            //    });

            //    //app.UseSpa(spa =>
            //    //{
            //    //    // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //    //    // see https://go.microsoft.com/fwlink/?linkid=864501

            //    //    spa.Options.SourcePath = "ClientApp";

            //    //    if (env.IsDevelopment())
            //    //    {
            //    //        spa.UseAngularCliServer(npmScript: "start");
            //    //    }
            //    //});

            app.UseMvc();


        }
    }
}
