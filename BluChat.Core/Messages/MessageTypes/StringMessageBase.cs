using BluChat.Core.Logger;
using BluChat.Core.Messages.Abstracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Messages.Data;

namespace BluChat.Core.Messages.MessageTypes
{
    public class StringMessageBase : MessageBase
    {
        public string Content { get; set; } = "Test";
        public override void MessangeHandler(MessageManager manager)
        {
            base.Sender.FindUser(manager);
            manager.Logger.Add(LogFactory.StringMessageRecieved(Sender.User, Content));
        }

        public override Message Convert()
        {
            return new Message()
            {
                Id = Guid.NewGuid(),
                ParentChat = ParentChat,
                Sender = Sender.User,
                UnformatedMessage = Content
            };
        }

        public static StringMessageBase Convert(MessageBase messageBase)
        {
            if (messageBase is StringMessageBase)
            {
                return (StringMessageBase)messageBase;
            }

            throw new InvalidOperationException("špatny typ");
        }
    }
}
