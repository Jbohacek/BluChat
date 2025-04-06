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
    public class RequestToGetChatMessages : MessageBaseServer
    {
        public SenderUser sender { get; set; }
        public Chat chat { get; set; }

        public override void MessangeHandler(MessageServerManager serverManager)
        {
            var database = serverManager.Database;
            var serializer = serverManager.serializer;
            var logger = serverManager.Logger;
            var user = database.Users.GetFirstOrDefault(x => x.Id == sender.Id);

            List<Message> messages = database.Messages.GetWhere(x => x.ParentChat.Id == chat.Id).ToList();

            ReturnMultipleStringMessages returnMultiple = new ReturnMultipleStringMessages();
            returnMultiple.Content = messages.Select(x => x.UnformatedMessage).ToList();

            string sendMultipleSeriazed = serializer.SerializeMessageToString(returnMultiple);

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


            serverManager.Server.Send(sender.IpPort, sendMultipleSeriazed);

        }

        public override Message Convert()
        {
            throw new NotImplementedException();
        }
    }
}
