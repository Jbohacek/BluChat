using BluChat.Core.Messages.Abstracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Common.Entities;
using BluChat.Core.Infrastructure.Logger;

namespace BluChat.Core.Messages.MessageTypes
{
    public class StringMessage : MessageBaseServer
    {
        public string Content { get; set; } = "Test";

        public StringMessage()
        {
            ToSave = true;
        }
        
        public override void MessangeHandler(MessageServerManager serverManager)
        {
            base.Sender.FindUser(serverManager);
            serverManager.Logger.Add(LogFactory.StringMessageRecieved(Sender.User, Content));
            serverManager.AddMessageToDatabase(this);
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

        public static StringMessage Convert(MessageBaseServer messageBaseServer)
        {
            if (messageBaseServer is StringMessage)
            {
                return (StringMessage)messageBaseServer;
            }

            throw new InvalidOperationException("špatny typ");
        }
    }
}
