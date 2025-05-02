using System.Xml.Serialization;
using System.Reflection;
using BluNoro.Core.Messages.Abstracts;

namespace BluNoro.Core.Messages
{
    public class MessageSerializer
    {
        private static XmlSerializer CreateServerSerializer()
        {
            var baseType = typeof(MessageBaseServer);
            var derivedTypes = Assembly.GetAssembly(baseType)
                .GetTypes()
                .Where(t => t.IsSubclassOf(baseType))
                .ToArray();

            return new XmlSerializer(baseType, derivedTypes);
        }

        private XmlSerializer serverSerializer = CreateServerSerializer();


        private static XmlSerializer CreateClientSerializer()
        {
            var baseType = typeof(MessageBaseClient);
            var derivedTypes = Assembly.GetAssembly(baseType)
                .GetTypes()
                .Where(t => t.IsSubclassOf(baseType))
                .ToArray();

            return new XmlSerializer(baseType, derivedTypes);
        }

        private XmlSerializer clientSerializer = CreateClientSerializer();


        public string SerializeMessageToString(MessageBaseServer messageBaseServer)
        {
            using (StringWriter writer = new StringWriter())
            {
                serverSerializer.Serialize(writer, messageBaseServer);
                return writer.ToString();
            }
        }

        public string SerializeMessageToString(MessageBaseClient messageBaseClient)
        {
            using (StringWriter writer = new StringWriter())
            {
                clientSerializer.Serialize(writer, messageBaseClient);
                return writer.ToString();
            }
        }

        public MessageBaseServer DeserializeServerMessageFromString(string messageString)
        {
            using (StringReader reader = new StringReader(messageString))
            {
                return (MessageBaseServer)serverSerializer.Deserialize(reader);
            }
        }

        public MessageBaseClient DeserializeClientMessageFromString(string messageString)
        {
            using (StringReader reader = new StringReader(messageString))
            {
                return (MessageBaseClient)clientSerializer.Deserialize(reader);
            }
        }
    }
}
