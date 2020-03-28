using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using TC.BrowserEngine.AdminPanel;
using TC.BrowserEngine.AdminPanel.DataAccess;
using TC.BrowserEngine.Helpers;

namespace TC.BrowserEngine.Signal
{
    public class SignalClientBase
    {
        private string _hubName;
        private LocalUserRepository _localUserRepository;
        private HubConnection connection;
        public SignalClientBase(string hubName)
        {
            _hubName = hubName;
            _localUserRepository = new LocalUserRepository();
        }
        protected async Task<HubConnection> StartAsync()
        {
            try
            {
                string token = _localUserRepository.GetToken();
                while (token == null)
                {
                    Console.WriteLine("Server is not avaiable reconnent in 5s");
                    await Task.Delay(5000);
                    token =  Login.LoginAsync("test@test", "test");
                }

                connection = new HubConnectionBuilder()
                      .WithUrl($"{ConfigHelper.GetServerAddress()}hubs/{_hubName}?t=c", options =>
                        {
                            options.AccessTokenProvider = () => Task.FromResult(token);
                        })// t=c it's type console 
                          .WithAutomaticReconnect()
                      .Build();
                connection.Closed += async (error) =>
                {
                    await Task.Delay(5000);
                    await connection.StartAsync();
                };
                connection.Reconnected += async (info) =>
                {
                    Console.WriteLine("Connection Reconnected");
                  
                    // await Task.Delay(new Random().Next(0, 5) * 1000);
                    // await connection.StartAsync();
                };
                 connection.Reconnecting += async (info) =>
                {
                    // await Task.Delay(new Random().Next(0, 5) * 1000);
                    // await connection.StartAsync();
                    Console.WriteLine("Connection Reconnecting");
                   
                };

                //await connection.StartAsync();

                await connection.StartAsync();

                Console.WriteLine("Connection started");
                return connection;

            }
            catch (HubException ex)
            {

                Console.WriteLine(ex);
                return null;
            }
            catch (Exception ex)
            {
                if (ex.Message == "Response status code does not indicate success: 401 (Unauthorized).")
                {
                   _localUserRepository.LogoutCurrentUser();
                    LocalServer.Instance.OpenLoginPage();

                }
                // {
                //No connection could be made because the target machine actively refused it.
                // }
                Console.WriteLine(ex);
                return null;
            }
        }

    }
}
