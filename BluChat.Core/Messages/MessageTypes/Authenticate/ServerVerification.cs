using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Logger;
using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.Data;
using BluChat.Core.Messages.SenderReciever;
using BluChat.Core.UserFolder;

namespace BluChat.Core.Messages.MessageTypes.Authenticate
{
    public class ServerVerification : MessageBaseServer
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public override void MessangeHandler(MessageServerManager serverManager)
        {
            User? possibleUser = serverManager.Database.Users.GetAll().FirstOrDefault(x => x.UserName == UserName);

            //Check if user exists
            if (possibleUser == null)
            {
                serverManager.Logger.Add(LogFactory.Authentication.UserNotFoundByUsername(UserName,Sender.IpPort));
                var failed = GenerateFailed("User not found");
                var serilezed = serverManager.serializer.SerializeMessageToString(failed);
                serverManager.Server.Send(base.Sender.IpPort, serilezed);
                return;
            }

            //Check if password matches
            if (!PasswordManager.VerifyPassword(Password, possibleUser.HashPassword))
            {
                serverManager.Logger.Add(LogFactory.Authentication.WrongPassword(UserName,Sender.IpPort));
                var failed = GenerateFailed("Wrong password");
                var serilezed = serverManager.serializer.SerializeMessageToString(failed);
                serverManager.Server.Send(base.Sender.IpPort, serilezed);
                return;
            }

            //Generate responce
            var success = GenerateSuccess(possibleUser);
            var serilzed = serverManager.serializer.SerializeMessageToString(success);
            serverManager.Logger.Add(LogFactory.Authentication.Authenticated(possibleUser,base.Sender.IpPort));

            // Return to client
            serverManager.Server.Send(base.Sender.IpPort, serilzed);
            possibleUser.ServerStatus = new UserServerStatus(base.Sender.IpPort.ToString(), DateTime.Now)
                {
                    IsConnected = true
                };

            //Remove anoymous and replace with user
            serverManager.Parent.AnonymousUsers.RemoveAll(x => x.Adress.ToString() == base.Sender.IpPort.ToString());
            serverManager.Parent.ConnectedUsers.Add(possibleUser);

            serverManager.Logger.Add(LogFactory.UserConnected(possibleUser));
        }

        private ClientSuccessVerification GenerateSuccess(User succesUser)
        {
            return new ClientSuccessVerification()
            {
                authenticatedUser = succesUser,
                SendTime = DateTime.Now
            };
        }

        private ClientFailedVerification GenerateFailed(string reason)
        {
            return new ClientFailedVerification()
            {
                Reason = reason,
                Sender = base.Sender,
                SendTime = DateTime.Now
            };
        }

        public override Message Convert()
        {
            throw new NotImplementedException();
        }
    }
}
