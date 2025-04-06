using BluChat.Core.Messages.MessageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Messages.MessageTypes.GetChats;

namespace BluChat.Core.Messages
{
    public class MessageFactory
    {
        public static StringMessage GetTestMessage()
        {
            StringMessage message = new StringMessage();
            message.Content = "Test";



            return message;
        }


    }
}
