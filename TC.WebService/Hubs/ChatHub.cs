using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TC.WebService.Hubs
{
    public class ChatHub : Hub
    {
        
        public override Task OnConnectedAsync()
        {
            var myInfo = Context;
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
          

            return base.OnDisconnectedAsync(exception);
        }
        #region snippet_SendMessage
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        #endregion
    }
}
