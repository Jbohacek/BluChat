using BluChat.Core.Data;
using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.MessageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Logger;
using Microsoft.Extensions.Logging;
using ILogger = BluChat.Core.Logger.Interfaces.ILogger;

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
            MessageBase messageBase = serializer.DeserializeMessageFromString(messageString);
            messageBase.MessangeHandler(this);

            AddMessageToDatabase(messageBase);

            return false;
        }

        public void AddMessageToDatabase(MessageBase messageBase)
        {
            var x = messageBase.Convert();

            Database.Messages.Add(messageBase.Convert());
            Database.Save();
        }


    }
}
