using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.Data;

namespace BluChat.Core.Messages.MessageTypes.GetChats
{
    public class ClientReturnChats : MessageBaseClient
    {
        public List<Chat> Chats { get; set; }

        public override void MessangeHandler(MessageClientManager clientManager)
        {
            clientManager.Client.Events.OnChatsRecieved(this);
        }
    }
}
