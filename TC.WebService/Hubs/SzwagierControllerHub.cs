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
            try
            {
                //TODO do case wgeb testrunhistory is null
                if (message.TestInfoId != null)
                {
                    TestRunHistory testRunHistory = new TestRunHistory()
                    {
                        TestInfoId = message.TestInfoId.Value,
                        SelectedBrowserEngine = message.ReceiverConnectionId,
                        Configuration = JsonSerializer.Serialize(message.Configurations)
                    };
                    _testRunHistoryRepository.Create(testRunHistory);
                    _unitOfWork.SaveChanges();
                    message.TestRunHistoryId = testRunHistory.Id;
                }
                message.SenderConnectionId = Context.ConnectionId;
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
            //try
            //{
            //TODO do case wgeb testrunhistory is null
            if (message.TestRunHistoryId != null)
            {
                _testRunResultRepository.Create(new TestRunResult()
                {
                    TestRunHistoryId = message.TestRunHistoryId.Value,
                    CommandTestGuid = message.CommandTestGuid,
                    IsSuccesful = message.IsSuccesful
                });
                _unitOfWork.SaveChanges();
            }

            await Clients.Client(message.SenderConnectionId).SendAsync("TestProgress", message);
            //}
            //catch (Exception ex)
            //{
            //    //TODO log to db

            //    throw ex;
            //}
        }
    }
}
