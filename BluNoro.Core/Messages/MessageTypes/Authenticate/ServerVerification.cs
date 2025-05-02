using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Infrastructure;
using BluNoro.Core.Infrastructure.Logger;
using BluNoro.Core.Messages.Abstracts;

namespace BluNoro.Core.Messages.MessageTypes.Authenticate
{
    public class ServerVerification : MessageBaseServer
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public override void MessangeHandler(MessageServerManager serverManager)
        {
            User? possibleUser = serverManager.Database.Users.GetAll("Chats").FirstOrDefault(x => x.UserName == UserName);

            //Check if user exists
            if (possibleUser == null)
            {
                //serverManager.Logger.Add(LogFactory.Authentication.UserNotFoundByUsername(UserName,Sender.IpPort));
                //var failed = GenerateFailed("User not found");
                //var serilezed = serverManager.serializer.SerializeMessageToString(failed);
                //serverManager.Server.Send(base.Sender.IpPort, serilezed);
                Log log = LogFactory.Authentication.UserNotFoundByUsername(UserName, Sender.IpPort);
                SendFailed(serverManager, log, "User not Found");
                return;
            }

            //Check if password matches
            if (!PasswordManager.VerifyPassword(Password, possibleUser.HashPassword))
            {
                //serverManager.Logger.Add(LogFactory.Authentication.WrongPassword(UserName,Sender.IpPort));
                //var failed = GenerateFailed("Wrong password");
                //var serilezed = serverManager.serializer.SerializeMessageToString(failed);
                //serverManager.Server.Send(base.Sender.IpPort, serilezed);
                Log log = LogFactory.Authentication.WrongPassword(UserName, Sender.IpPort);
                SendFailed(serverManager, log, "Wrong password");
                return;
            }

            //Check if user is already connected
            if(serverManager.Parent.ConnectedUsers.Any(x => x.Id == possibleUser.Id))
            {
                Log log = LogFactory.Authentication.AlreadyConnected(possibleUser, Sender.IpPort);
                SendFailed(serverManager, log, "User already connected");
                return;
            }

            //Generate responce
            var success = GenerateSuccess(possibleUser);
            var serilzed = serverManager.serializer.SerializeMessageToString(success);
            serverManager.Logger.Add(LogFactory.Authentication.Authenticated(possibleUser, Sender.IpPort));

            // Return to client
            serverManager.Server.Send(Sender.IpPort, serilzed);
            possibleUser.ServerStatus = new UserServerStatus(Sender.IpPort.ToString(), DateTime.Now)
                {
                    IsConnected = true
                };

            //Remove anoymous and replace with user
            serverManager.Parent.AnonymousUsers.RemoveAll(x => x.Adress.ToString() == Sender.IpPort.ToString());
            serverManager.Parent.ConnectedUsers.Add(possibleUser);

            serverManager.Logger.Add(LogFactory.UserConnected(possibleUser));
        }

        private void SendFailed(MessageServerManager serverManager, Log why, string reason)
        {
            serverManager.Logger.Add(why);
            var failed = GenerateFailed(reason);
            var serilezed = serverManager.serializer.SerializeMessageToString(failed);
            serverManager.Server.Send(Sender.IpPort, serilezed);
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
                SendTime = DateTime.Now
            };
        }

        public override Message Convert()
        {
            throw new NotImplementedException();
        }
    }
}
