using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Infrastructure.Logger;
using BluNoro.Core.Messages.Abstracts;

namespace BluNoro.Core.Messages.MessageTypes
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
            Sender.FindUser(serverManager);
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
