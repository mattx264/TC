using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using TC.Common.DTO;

namespace TC.WebService.Hubs
{
   
    public partial class SzwagierHub : Hub
    {
        public async Task SendCommand(CommandMessage message)
        {
            // TODO check if sender can make call to reciver (browser engine)
            // TODO add to testHistory
            message.SenderConnectionId = Context.ConnectionId;
            await Clients.Client(message.ReceiverConnectionId).SendAsync("ReciveCommand", message);
        }
        public async Task TestProgress(TestProgressMessage message)
        {
            // TODO add to testHistory
            await Clients.Client(message.SenderConnectionId).SendAsync("TestProgress", message);
        }
        
        public async Task SendScreenShot(TestProgressImage message)
        {
            //1. Save image
            //2. Send Url to client

            // TODO add to testHistory
            //await Clients.Client(message.SenderConnectionId).SendAsync("SendScreenShot", message);
        }
    }
}
