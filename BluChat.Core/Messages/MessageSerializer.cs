using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.MessageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BluChat.Core.Messages
{
    public class MessageSerializer
    {
        private XmlSerializer serializer = new XmlSerializer(typeof(MessageBase), new Type[] { typeof(StringMessageBase) });

        public string SerializeMessageToString(MessageBase messageBase)
        {
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, messageBase);
                return writer.ToString();
            }
        }

        public MessageBase DeserializeMessageFromString(string messageString)
        {
            using (StringReader reader = new StringReader(messageString))
            {
                return (MessageBase)serializer.Deserialize(reader);
            }
        }
    }
}
