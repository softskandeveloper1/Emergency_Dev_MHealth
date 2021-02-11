using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DAL.Models;
using DAL.Utils;
using System.Security.Claims;

namespace MHealth.Hub
{
    public class Chat : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendMessage(string user, string message)
        {
            var conversation = new mp_conversation
            {
                from = Context.UserIdentifier,
                to = user,
                message=message
            };
            ConversationUtil.AddConversation(conversation);

            await Clients.User(user).SendAsync(user, message);
            //await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message);
        }


        public Task Join()
        {

            return Groups.AddToGroupAsync(Context.ConnectionId, Context.UserIdentifier);
        }

        public string GetConnectionId()
        {

            return Context.ConnectionId;
        }
    }
}
