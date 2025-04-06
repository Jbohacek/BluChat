using BluChat.Core.Data;
using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.MessageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Logger;
using BluChat.Core.Messages.Data;
using BluChat.Core.UserFolder;
using Microsoft.Extensions.Logging;
using ILogger = BluChat.Core.Logger.Interfaces.ILogger;
using BluChat.Core.Messages.SenderReciever;
using BluChat.Core.ServerFolder;
using SuperSimpleTcp;

namespace BluChat.Core.Messages
{
    /// <summary>
    /// Slouží k posílání a uchově zpráv na serveru
    /// </summary>
    public class MessageServerManager(UnitOfWork database, ILogger logger, SimpleTcpServer server,Server parent)
    {
        public SimpleTcpServer Server = server;
        public Server Parent = parent;
        public UnitOfWork Database { get; set; } = database;
        public ILogger Logger { get; set; } = logger;
        public readonly MessageSerializer serializer = new MessageSerializer();


        public bool RecieveMessage(string messageString, string ipPort)
        {
            MessageBaseServer messageBaseServer = serializer.DeserializeServerMessageFromString(messageString);
            messageBaseServer.Sender.IpPort = ipPort;
            messageBaseServer.MessangeHandler(this);

            if(messageBaseServer.ToSave)
                AddMessageToDatabase(messageBaseServer);

            return true;
        }

        public void AddMessageToDatabase(MessageBaseServer messageBaseServer)
        {
            var x = messageBaseServer.Convert();

            Database.Messages.Add(messageBaseServer.Convert());
            Database.Save();
        }


    }
}
