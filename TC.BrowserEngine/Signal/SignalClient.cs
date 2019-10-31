//using Microsoft.AspNetCore.SignalR.Client;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using TC.BrowserEngine.Controllers;
//using TC.BrowserEngine.Helpers.Enums;
//using TC.Common.Selenium;

//namespace TC.BrowserEngine.Signal
//{
//    public class SignalClient
//    {
//        public SignalClient()
//        {
            
//        }
//        public async Task StartAsync()
//        {
//            string token = await Login.LoginAsync("test@test", "test");

//            var connection = new HubConnectionBuilder()
//              .WithUrl("https://localhost:44384/hubs/szwagier?t=c", options =>
//              {
//                  options.AccessTokenProvider = () => Task.FromResult(token);
//              })
//              //.WithUrl("http://localhost:53353/ChatHub")
//              //.WithAutomaticReconnect() -> when .net core 3.0
//              .Build();

//            connection.Closed += async (error) =>
//            {
//                await Task.Delay(new Random().Next(0, 5) * 1000);
//                await connection.StartAsync();
//            };
//            //await connection.StartAsync();
//            try
//            {
//                connection.StartAsync().GetAwaiter();
//                Console.WriteLine("Connection started");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex);
//            }
//            connection.On("ReciveTriggerTest", (int testId,  List<SeleniumCommand> commands) =>
//            {
//                Console.WriteLine(testId);
//                Console.WriteLine(testId);
//                var browserController = new BrowserController(BrowserType.Chrome);
//                browserController.Start().GetAwaiter();
//                browserController.RunCommandProcessor(commands);
//            });
//            Console.WriteLine("Click esc to stop, click enter to send and start new message.");

//            do
//            {
//                var mes = Console.ReadLine();
//              //  if ((Key)mes == ConsoleKey.Escape)
//               // {
////
//          //     }
//                connection.InvokeAsync("SendMessage", "Consloe Client", mes).GetAwaiter();
//            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
           
//        }
//    }
//}
