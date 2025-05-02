using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Common.Entities;
using BluChat.Core.Messages.Abstracts;

namespace BluChat.Core.Messages.MessageTypes.SendMessage.Confirmation
{
    public class ClientSuccessSendMessage : MessageBaseClient
    {
        public Message Message { get; set; }

        public ClientSuccessSendMessage(Message message)
        {
            Message = message;
        }

        public ClientSuccessSendMessage()
        {
            
        }

        public override void MessangeHandler(MessageClientManager clientManager)
        {
            
        }
    }
}
