using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Messages.Abstracts;

namespace BluNoro.Core.Messages.MessageTypes.SendMessage.Confirmation
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
