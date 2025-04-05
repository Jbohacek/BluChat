using BluChat.Core.Messages.MessageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Messages
{
    public class MessageFactory
    {
        public static StringMessageBase GetTestMessage()
        {
            StringMessageBase messageBase = new StringMessageBase();
            messageBase.Content = "Test";



            return messageBase;
        }
    }
}
