using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.Data;

namespace BluChat.Core.Messages.MessageTypes.SendMessage
{
    public class ClientBroadcastMessage : MessageBaseClient
    {
        public Guid ParentIdChat { get; set; }
        public Message Message { get; set; } 

        public ClientBroadcastMessage()
        {
            
        }

        public ClientBroadcastMessage(Guid parentChat, Message messaga)
        {
            ParentIdChat = parentChat;
            Message = messaga;
        }

        public override void MessangeHandler(MessageClientManager clientManager)
        {
            clientManager.Client.Events.OnMessageRecieved(this);
        }
    }
}
