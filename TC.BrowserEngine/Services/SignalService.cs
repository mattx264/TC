using PeterKottas.DotNetCore.WindowsService.Base;
using PeterKottas.DotNetCore.WindowsService.Interfaces;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TC.BrowserEngine.Controllers;
using TC.BrowserEngine.Signal;

namespace TC.BrowserEngine.Services
{
    public class SignalService : MicroService, IMicroService, IDisposable
    {
        BrowserControllerPlug browserControllerPlug;
        public void Start()
        {
            browserControllerPlug = new BrowserControllerPlug("szwagier", new BrowserControllerQueue());
            this.StartBase();
            Timers.Start("Poller", 1000 *60, () =>
            {
                Console.WriteLine("Polling at {0}\n", DateTime.Now.ToString("o"));
            },
            (e) =>
            {
                Console.WriteLine("Exception while polling: {0}\n", e.ToString());
            });
            Console.WriteLine("I started");
        }

        public void Stop()
        {
            this.StopBase();
            Console.WriteLine("I stopped");

        }
        public new void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
