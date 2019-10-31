using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace TC.WebService.Hubs
{
    //[Authorize]

    public partial class SzwagierHub : Hub
    {
        public async Task SendCommand(Object obj)
        {
            await Clients.All.SendAsync("ReciveCommand", obj);
        }
        public async Task SendScreenShot(byte[] image)
        {

        }
    }
}
