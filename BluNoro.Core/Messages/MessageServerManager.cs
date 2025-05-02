using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSimpleTcp;
using BluNoro.Core.Infrastructure;
using BluNoro.Core.Infrastructure.Logger.Interfaces;
using BluNoro.Core.Messages.Abstracts;
using BluNoro.Core.Networking;

namespace BluNoro.Core.Messages
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
