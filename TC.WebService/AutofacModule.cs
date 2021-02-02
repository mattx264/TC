using Autofac;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Linq;
using TC.DataAccess;

namespace TC.WebService
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method. All of this starts
            // with the `UseServiceProviderFactory(new AutofacServiceProviderFactory())` that happens in Program and registers Autofac
            // as the service provider.
            /* builder.Register(c => new ValuesService(c.Resolve<ILogger<ValuesService>>()))
                 .As<IValuesService>()
                 .InstancePerLifetimeScope();*/
            //builder.RegisterType<HttpContextAccessor>()
            //    .As<IHttpContextAccessor>()
            //    .SingleInstance();

            var dataAccess = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains("TC.DataAccess"));

            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(dataAccess)
               .Where(t => t.Name.EndsWith("UnitOfWork"))
               .AsImplementedInterfaces();

            //builder.Register(c => new UnitOfWork(c.Resolve<PewexDbContext>(), c.ResolveService()))
            //    .As<IUnitOfWork>()
            //   .InstancePerLifetimeScope();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Helper") || t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces();


        }
    }
    public static class RedisMultiplexer
    {
        public static IServiceCollection AddRedisMultiplexer(
                this IServiceCollection services,
                Func<ConfigurationOptions> getOptions = null)
        {
            if (getOptions is null)
            {
                throw new ArgumentNullException(nameof(getOptions));
            }
            // Get the options or assume localhost, as these will be set in Startup.ConfigureServices assume they won't change
            var options = getOptions?.Invoke() ?? ConfigurationOptions.Parse("localhost");

            // The Redis is a singleton, shared as much as possible.
            return services.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(options));
        }
    }
}
