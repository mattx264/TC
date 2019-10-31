using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using TC.BrowserEngine.Helpers;

namespace TC.BrowserEngine.Signal
{
    public class SignalClientBase
    {
        private string _hubName;
        private HubConnection connection;
        public SignalClientBase(string hubName)
        {
            _hubName = hubName;
        }
        protected async Task<HubConnection> StartAsync()
        {
            try
            {
                string token = await Login.LoginAsync("test@test", "test");
                while (token == null)
                {
                    Console.WriteLine("Server is not avaiable reconnent in 5s");
                    await Task.Delay(5000);
                    token =await Login.LoginAsync("test@test", "test");
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
            catch (Exception ex)
            {
                // if (ex.NativeErrorCode == 10061)
                // {
                //No connection could be made because the target machine actively refused it.
                // }
                Console.WriteLine(ex);
                return null;
            }
        }

    }
}
