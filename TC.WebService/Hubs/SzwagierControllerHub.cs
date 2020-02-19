using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.Text.Json;
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
            try
            {

                if (message.TestInfoId == null)
                {
                    throw new Exception("TestInfoId is required");
                }

                TestRunHistory testRunHistory = new TestRunHistory()
                {
                    TestInfoId = message.TestInfoId.Value,
                    SelectedBrowserEngine = message.ReceiverConnectionId,
                    Configuration = JsonSerializer.Serialize(message.Configurations)
                };
                _testRunHistoryRepository.Create(testRunHistory);
                _unitOfWork.SaveChanges();

                message.SenderConnectionId = Context.ConnectionId;
                message.TestRunHistoryId = testRunHistory.Id;
                await Clients.Client(message.ReceiverConnectionId).SendAsync("ReciveCommand", message);
            }
            catch (Exception ex)
            {
                //TODO log to db

                throw ex;
            }
        }
        public async Task TestProgress(TestProgressMessage message)
        {
            try
            {
                if (message.TestRunHistoryId == 0)
                {
                    throw new Exception("TestProgress TestRunHistoryId is 0");
                }
                _testRunResultRepository.Create(new TestRunResult()
                {
                    TestRunHistoryId = message.TestRunHistoryId,
                    CommandTestGuid = message.CommandTestGuid,
                    IsSuccesful = message.IsSuccesful
                });
                _unitOfWork.SaveChanges();
                await Clients.Client(message.SenderConnectionId).SendAsync("TestProgress", message);
            }
            catch (Exception ex)
            {
                //TODO log to db

                throw ex;
            }
        }
    }
}
