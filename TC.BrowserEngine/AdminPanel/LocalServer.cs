using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TC.BrowserEngine.AdminPanel
{
    public sealed  class LocalServer
    {
        private static readonly Lazy<LocalServer>
               lazy = new Lazy<LocalServer>
               (() => new LocalServer());

        public static LocalServer Instance { get { return lazy.Value; } }

       
        private LocalServer()
        {
            host = new WebHostBuilder()
             .UseKestrel()
             .UseContentRoot(Directory.GetCurrentDirectory())
             //.UseIISIntegration()
             .UseStartup<Startup>()
             .UseUrls("http://*:54321")
             .Build();

            host.Start();
        }       
        private IWebHost host;

        public void OpenLoginPage()
        {
            OpenBrowser("http://localhost:54321/#!/login");
        }
        public void OpenBrowser(string url)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url.Replace("&", "^&")}")); // Works ok on windows and escape need for cmd.exe
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);  // Works ok on linux
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url); // Not tested
            }
            else
            {
                throw new Exception("Operator system on known");
            }
        }
    }
}
