using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Logger;
using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.Data;
using BluChat.Core.Messages.SenderReciever;
using BluChat.Core.ServerFolder;
using BluChat.Core.UserFolder;

namespace BluChat.Core.Messages.MessageTypes.GetChatMessages
{
    public class ServerRequestToGetChatMessages : MessageBaseServer
    {
        public Chat chat { get; set; }

        public override void MessangeHandler(MessageServerManager serverManager)
        {
            var database = serverManager.Database;
            var serializer = serverManager.serializer;
            var logger = serverManager.Logger;
            var user = database.Users.GetFirst(x => x.Id == Sender.User.Id);

            List<Message> messages = database.Messages.GetAll("Sender").Where(x => x.ParentChat.Id == chat.Id).ToList();

            ClientMultipleStringMessages clientMultiple = new ClientMultipleStringMessages();
            clientMultiple.Content = messages.ToList();
            clientMultiple.idChat = chat.Id;

            string sendMultipleSeriazed = serializer.SerializeMessageToString(clientMultiple);

            if (user.ServerStatus == null)
            {
                 logger.Add(LogFactory.UserNotFoundToSend(user));
                 return;
            }

            if (user.ServerStatus.IsConnected == false)
            {
                 logger.Add(LogFactory.UserNotFoundToSend(user));
                 return;
            }


            serverManager.Server.Send(Sender.IpPort, sendMultipleSeriazed);

        }

        public override Message Convert()
        {
            throw new NotImplementedException();
        }
    }
}
