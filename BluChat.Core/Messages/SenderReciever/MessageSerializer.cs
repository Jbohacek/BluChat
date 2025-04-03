using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.MessageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BluChat.Core.Messages.SenderReciever
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
    }
}
