using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.Data;

namespace BluChat.Core.Messages.MessageTypes.GetChats
{
    public class ReturnChats : MessageBaseServer
    {
        public List<Chat> Chats { get; set; }

        public override void MessangeHandler(MessageServerManager serverManager)
        {
            serverManager.Server.Send(base.Sender.IpPort, serverManager.serializer.SerializeMessageToString(this));
        }

        public override Message Convert()
        {
            throw new NotImplementedException();
        }
    }
}
