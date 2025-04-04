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
        private XmlSerializer serializer = new XmlSerializer(typeof(Message), new Type[] { typeof(StringMessage) });

        public string SerializeMessageToString(Message message)
        {
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, message);
                return writer.ToString();
            }
        }

        public Message DeserializeMessageFromString(string messageString)
        {
            using (StringReader reader = new StringReader(messageString))
            {
                return (Message)serializer.Deserialize(reader);
            }
        }
    }
}
