using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.ClientFolder;
using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.Data;
using BluChat.Core.Messages.MessageTypes.Authenticate;
using BluChat.Core.Messages.MessageTypes.GetChatMessages;
using BluChat.Core.Messages.SenderReciever;
using BluChat.Core.UserFolder;
using SuperSimpleTcp;

namespace BluChat.Core.Messages
{
    public class MessageClientManager(SimpleTcpClient simpleclient, Client client)
    {
        public SimpleTcpClient SimpleTcp = simpleclient;
        public Client Client = client;
        public MessageSerializer Serializer = new MessageSerializer();
        private bool IsConnected => simpleclient.IsConnected;

        public void SendAuthentication(string username, string password)
        {
            var request = new ServerVerification()
            {
                Password = password,
                Sender = client.Sender,
                SendTime = DateTime.Now,
                UserName = username
            };
            if(!IsConnected) return;

            var serlized = request.SerlizeMe(Serializer);

            SimpleTcp.Send(serlized);

        }

        public void GetChatMessages(Chat chat)
        {
            ServerRequestToGetChatMessages request = new ServerRequestToGetChatMessages()
            {
                SendTime = DateTime.Now,
                Sender = Client.Sender,
                chat = chat
            };

            var serilized = request.SerlizeMe(Serializer);

            SimpleTcp.Send(serilized);
        }

        public void RecieveMessage(string message)
        {
            MessageBaseClient messageBaseClient = Serializer.DeserializeClientMessageFromString(message);
            messageBaseClient.MessangeHandler(this);
        }


    }
}
