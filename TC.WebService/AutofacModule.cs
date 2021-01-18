using Autofac;
using System;
using System.Linq;

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
            // builder.Register(c => new UnitOfWork(c.Resolve<PewexDbContext>(), c.ResolveService()))
            //     .As<IUnitOfWork>()
            //    .InstancePerLifetimeScope();


            var helpers = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(helpers)
                   .Where(t => t.Name.EndsWith("Helper"))
                   .AsImplementedInterfaces();
        }
    }
}
