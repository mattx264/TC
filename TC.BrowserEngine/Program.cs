using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using PeterKottas.DotNetCore.WindowsService;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TC.BrowserEngine.Controllers;
using TC.BrowserEngine.Helpers;
using TC.BrowserEngine.Helpers.Enums;
using TC.BrowserEngine.Services;
using TC.Common.Selenium;

namespace TC.BrowserEngine
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
          
            //var provider = new PhysicalFileProvider(@"C:\Users\mmachaj\source\repos\TestingCenter\TC.BrowserEngine\ImageTemp");
            //var contents = provider.GetDirectoryContents(string.Empty);
            //var fileInfo = provider.GetFileInfo("temp.jpg");

            //await new FileUploadHelper().UploadImageAsync(fileInfo.CreateReadStream(), "test.jpg");

            string password = ConsoleHelper.GetValue(args, "test");
            string email = ConsoleHelper.GetValue(args, "test@test");
            // Login to server with JWT

            ServiceRunner<SignalService>.Run(config =>
            {
                string name = "POL.TC.BrowserEngine";
                config.SetName(name);
                config.SetDescription("POL.TC.BrowserEngine service is background job that is doing what have to be done.");

                config.Service(serviceConfig =>
                {
                    serviceConfig.ServiceFactory((extraArguments, microServiceController) =>
                    {
                        return new SignalService();
                    });
                    serviceConfig.OnStart((service, extraArguments) =>
                    {
                        Console.WriteLine("Service {0} started", name);
                        service.Start();
                    });

                    serviceConfig.OnStop(service =>
                    {
                        Console.WriteLine("Service {0} stopped", name);
                        service.Stop();
                    });

                    serviceConfig.OnInstall(service =>
                    {
                        Console.WriteLine("Service {0} installed", name);
                    });

                    serviceConfig.OnUnInstall(service =>
                    {
                        Console.WriteLine("Service {0} uninstalled", name);
                    });

                    serviceConfig.OnPause(service =>
                    {
                        Console.WriteLine("Service {0} paused", name);
                    });

                    serviceConfig.OnContinue(service =>
                    {
                        Console.WriteLine("Service {0} continued", name);
                    });

                    serviceConfig.OnShutdown(service =>
                    {
                        Console.WriteLine("Service {0} shutdown", name);
                    });

                    serviceConfig.OnError(e =>
                    {
                        Console.WriteLine("Service {0} errored with exception : {1}", name, e.Message);
                    });
                });
            });

        }

        public static IConfiguration SetupConfig()
        {
            IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", true, true)
           .Build();
            return config;
        }
    }
}
