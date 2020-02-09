using Microsoft.Extensions.Configuration;
using TC.BrowserEngine.AdminPanel;
using TC.BrowserEngine.AdminPanel.DataAccess;

namespace TC.BrowserEngine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var localServer = LocalServer.Instance;

            string token = new LocalUserRepository().GetToken();
            if (token == null)
            {
                localServer.OpenLoginPage();
            }
            BrowserEngineManager.Instance.StartNewInstance();


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
