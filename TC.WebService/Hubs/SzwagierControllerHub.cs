using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.Threading.Tasks;
using TC.Common.DTO;
using TC.WebService.Services;

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
            try
            {
                string filePath = await _fileManager.SaveFile(message.ImageBase64);
                await Clients.Client(message.SenderConnectionId).SendAsync("ReciveScreenshot", new TestProgressImageRespons
                {
                    ImagePath = filePath,
                    CommandTestGuid = message.CommandTestGuid
                });
            }
            catch (Exception ex)
            {
                throw new Exception("SendScreenshot", ex);
            }
        }
    }
}
