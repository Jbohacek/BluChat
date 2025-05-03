using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Client.Infrastructure;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Messages.Abstracts;

namespace BluNoro.Core.Common.MessageTypes.SendMessage
{
    public class ClientBroadcastMessage : MessageBaseBroadcast
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
