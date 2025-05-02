using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Messages;
using BluNoro.Core.Messages.Abstracts;

namespace BluNoro.Core.Messages.MessageTypes.GetChatMessages
{
    public class ClientMultipleStringMessages : MessageBaseClient
    {
        public List<Message> Content { get; set; } = new List<Message>();
        public Guid idChat { get; set; }

        public override void MessangeHandler(MessageClientManager clientManager)
        {
            if(clientManager.Client.Sender.User == null)
                return;

            Chat? clientChat = clientManager.Client.Sender.User.Chats.FirstOrDefault(x => x.Id == idChat);
            
            if(clientChat == null)
                return;

            clientChat.Messages.Clear();
            clientChat.Messages = Content;

            clientManager.Client.Events.OnGetChatMessages(this);

        }
    }
}
