using System;
using System.Collections.Generic;
using System.Text;

namespace TC.BrowserEngine
{
    public sealed class BrowserEngineManager
    {
        private static readonly Lazy<BrowserEngineManager>
            lazy = new Lazy<BrowserEngineManager>
            (() => new BrowserEngineManager());

        public static BrowserEngineManager Instance { get { return lazy.Value; } }

        private List<BrowserEngineStartup> browserEngineStartups;
        private BrowserEngineManager()
        {
            browserEngineStartups = new List<BrowserEngineStartup>();
        }
        public void StartNewInstance()
        {
            var browserEngineStartup = new BrowserEngineStartup();
            browserEngineStartups.Add(browserEngineStartup);
            browserEngineStartup.Start();
        }
        public void StartInstances()
        {
            if (browserEngineStartups.Count == 0)
            {
                StartNewInstance();

            }
            else
            {
                foreach (var item in browserEngineStartups)
                {
                    item.Service.Start();
                }
            } 
        }
        public void StopInstances()
        {
            foreach (var item in browserEngineStartups)
            {
                item.Service.Stop();
            }
            browserEngineStartups.Clear();
        }
    }
}
