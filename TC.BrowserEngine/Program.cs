using Microsoft.Extensions.Configuration;
using TC.BrowserEngine.AdminPanel;
using TC.BrowserEngine.AdminPanel.DataAccess;

namespace TC.BrowserEngine
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            //var provider = new PhysicalFileProvider(@"C:\Users\mmachaj\source\repos\TestingCenter\TC.BrowserEngine\ImageTemp");
            //var contents = provider.GetDirectoryContents(string.Empty);
            //var fileInfo = provider.GetFileInfo("temp.jpg");

            //await new FileUploadHelper().UploadImageAsync(fileInfo.CreateReadStream(), "test.jpg");


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
