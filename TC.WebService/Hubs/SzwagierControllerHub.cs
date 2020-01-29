using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.Threading.Tasks;
using TC.Common.DTO;
using TC.Entity.Entities;
using TC.WebService.Services;

namespace TC.WebService.Hubs
{

    public partial class SzwagierHub : Hub
    {

        public async Task SendCommand(CommandMessage message)
        {
            // TODO check if sender can make call to reciver (browser engine)
            // TODO add to testHistory
            if (message.TestInfoId != null)
            {
                _testRunHistoryRepository.Create(new TestRunHistory()
                {
                    TestInfoId = message.TestInfoId.Value
                });
                _unitOfWork.SaveChanges();
            }
            message.SenderConnectionId = Context.ConnectionId;
            await Clients.Client(message.ReceiverConnectionId).SendAsync("ReciveCommand", message);
        }
        public async Task TestProgress(TestProgressMessage message)
        {
            // TODO add to testHistory
            await Clients.Client(message.SenderConnectionId).SendAsync("TestProgress", message);
        }
    }
}
