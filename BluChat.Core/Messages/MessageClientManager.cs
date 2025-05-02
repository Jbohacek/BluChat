using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Common.Entities;
using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.MessageTypes.Authenticate;
using BluChat.Core.Messages.MessageTypes.GetChatMessages;
using BluChat.Core.Messages.MessageTypes.GetChats;
using BluChat.Core.Messages.MessageTypes.SendMessage;
using BluChat.Core.Messages.SenderReciever;
using BluChat.Core.Networking;
using Microsoft.EntityFrameworkCore.Query.Internal;
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

        public void SendMessage(string unformatedMessage, Chat chat)
        {
            if (!Client.HasUser)
            {
                throw new Exception("No user");
            }

            Message message = new Message()
            {
                Id = Guid.NewGuid(),
                ParentChat = chat,
                Sender = Client.Sender.User,
                UnformatedMessage = unformatedMessage
            };

            ServerRequestSendMessage messageRequest = new ServerRequestSendMessage();
            messageRequest.Message = message;
            messageRequest.Sender = Client.Sender;
            messageRequest.ParentChat = chat;
            messageRequest.SendTime = DateTime.Now;

            string serilizated = Serializer.SerializeMessageToString(messageRequest);
            SimpleTcp.Send(serilizated);



        }

        public void ReloadChats()
        {
            if (!client.HasUser)
                return;
            if (client.Sender.User == null)
                return;
            ServerRequestChats request = new(client.Sender.User.Id);
            request.Sender = new SenderUser();
            string serilzed = client.Manager.Serializer.SerializeMessageToString(request);
            client.Manager.SimpleTcp.Send(serilzed);

            Client.Events.ChatsRecieved += ((sender, args) =>
            {
                Client.Sender.User.Chats = args.Chats;
            });
        }
    }
}
