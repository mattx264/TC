using PeterKottas.DotNetCore.WindowsService;
using System;
using System.Collections.Generic;
using System.Text;
using TC.BrowserEngine.AdminPanel.DataAccess;
using TC.BrowserEngine.Services;

namespace TC.BrowserEngine
{
    public class BrowserEngineStartup
    {
        public SignalService Service { get; set; }
        public void Start()
        {
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
                        var user = new LocalUserRepository().GetCurrentUser();
                        if (user != null)
                        {
                            service.Start();
                        }
                        
                        Service = service;
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
    }
}
