using BluChat.Core.Data;
using BluChat.Core.Logger.Interfaces;
using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.MessageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Messages
{
    /// <summary>
    /// Slouží k posílání a uchově zpráv na serveru
    /// </summary>
    public class MessageManager
    {
        public UnitOfWork Database { get; set; }
        public ILogger Logger { get; set; }
        private readonly MessageSerializer serializer = new MessageSerializer();

        public MessageManager(UnitOfWork database, ILogger logger)
        {
            Database = database;
            Logger = logger;
        }

        public bool RecieveMessage(string messageString)
        {
            Message message = serializer.DeserializeMessageFromString(messageString);
            message.MessangeHandler(this);

            return false;
        }


    }
}
