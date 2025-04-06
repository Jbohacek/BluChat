using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.Data;

namespace BluChat.Core.Messages.MessageTypes.GetChats
{
    public class RequestChats : MessageBaseServer
    {

        public override void MessangeHandler(MessageServerManager serverManager)
        {
            List<Chat> chatList = serverManager.Database.Chats.GetAll().ToList();
            ReturnChats chatRet = new ReturnChats()
            {
                Chats = chatList,
                Sender = base.Sender,
                SendTime = DateTime.Now
            };
            chatRet.MessangeHandler(serverManager);

        }

        public override Message Convert()
        {
            throw new NotImplementedException();
        }
    }
}
