using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.Data;
using BluChat.Core.UserFolder;

namespace BluChat.Core.Messages.MessageTypes.GetChats
{
    public class ServerRequestChats : MessageBaseServer
    {
        public Guid UserId { get; set; }

        public ServerRequestChats() 
        {
            
        }

        public ServerRequestChats(Guid userId)
        {
            UserId = userId;
        }

        public override void MessangeHandler(MessageServerManager serverManager)
        {
            List<Chat> chatList = serverManager.Database.Chats.GetAll("Users").Where(x => x.Users.Any(z => z.Id == UserId)).ToList();
            ClientReturnChats chatRet = new ClientReturnChats()
            {
                Chats = chatList,
                SendTime = DateTime.Now
            };

            string serilized = serverManager.serializer.SerializeMessageToString(chatRet);

            serverManager.Server.Send(base.Sender.IpPort, serilized);

        }

        public override Message Convert()
        {
            throw new NotImplementedException();
        }
    }
}
