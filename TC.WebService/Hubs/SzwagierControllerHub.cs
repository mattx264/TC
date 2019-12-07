using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using TC.Common.DTO;

namespace TC.WebService.Hubs
{
   
    public partial class SzwagierHub : Hub
    {
        public async Task SendCommand(CommandMessage obj)
        {
            // TODO check if sender can make call to reciver (browser engine)
            // TODO add to testHistory
            obj.SenderConnectionId = Context.ConnectionId;
            await Clients.Client(obj.ReceiverConnectionId).SendAsync("ReciveCommand", obj);
        }
        public async Task TestProgress(TestProgressMessage message)
        {
            // TODO add to testHistory
            await Clients.Client(message.SenderConnectionId).SendAsync("TestProgress", message.IsSuccesful);
        }
        
        public async Task SendScreenShot(byte[] image)
        {

        }
    }
}
