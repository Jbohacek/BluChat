using System;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Contracts.Enums;
using BluNoro.Core.Infrastructure.Logger;
using BluNoro.Core.Messages.Abstracts;
using BluNoro.Core.Messages.MessageTypes.SendMessage.Confirmation;

namespace BluNoro.Core.Messages.MessageTypes.SendMessage
{
    public class ServerRequestSendMessage : MessageBaseServer
    {
        public Message Message { get; set; }
        public Chat ParentChat { get; set; }

        public ServerRequestSendMessage(Message message)
        {
            Message = message;
            ParentChat = message.ParentChat;
            ToSave = true;
        }

        public ServerRequestSendMessage()
        {
            ToSave = true;
        }

        public override void MessangeHandler(MessageServerManager serverManager)
        {
            //todo: Check if the messsage if verfied

            //Send success Send
            var returnSuccess = new ClientSuccessSendMessage(Message);
            var returnSuccessSerilizated = serverManager.serializer.SerializeMessageToString(returnSuccess);
            serverManager.Server.Send(Sender.IpPort.ToString(),returnSuccessSerilizated);

            //Broadcast new message
            var broadcast = new ClientBroadcastMessage(ParentChat.Id, Message);
            var broadcastSerilizated = serverManager.serializer.SerializeMessageToString(broadcast);

            var chatFromDatabase =
                serverManager.Database.Chats.GetAll("Users").FirstOrDefault(x => x.Id == ParentChat.Id);
            if (chatFromDatabase == null)
            {
                serverManager.Logger.Add(new Log("Chat not found",ParentChat.Id.ToString(), Enums.Level.ClientError));
                return;
            }

            var userList = chatFromDatabase.Users;
            var connectedUsers = serverManager.Parent.ConnectedUsers;

            foreach (var user in userList)
            {
                var connectedUser = connectedUsers.FirstOrDefault(x => x.Id == user.Id);
                if(connectedUser == null)
                    continue;
                if(connectedUser.ServerStatus == null)
                    continue;

                serverManager.Server.Send(connectedUser.ServerStatus.Adress.ToString(), broadcastSerilizated);

            }
        }

        public override Message Convert()
        {
            return new Message()
            {
                Id = Message.Id,
                ParentChat = Message.ParentChat,
                Sender = Message.Sender,
                UnformatedMessage = Message.UnformatedMessage
            };
        }
    }
}
