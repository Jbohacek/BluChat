using BluChat.Core.Logger;
using BluChat.Core.Messages.Abstracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Messages.MessageTypes
{
    public class StringMessage : Message
    {
        public string Content { get; set; } = "Test";
        public override void MessangeHandler(MessageManager manager)
        {
            base.Sender.FindUser(manager);
            manager.Logger.Add(LogFactory.StringMessageRecieved(Sender.User, Content));
        }

        public static StringMessage Convert(Message message)
        {
            if (message is StringMessage)
            {
                return (StringMessage)message;
            }

            throw new InvalidOperationException("špatny typ");
        }
    }
}
