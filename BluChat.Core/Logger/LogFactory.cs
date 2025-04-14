using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.UserFolder;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BluChat.Core.Logger
{
    public static class LogFactory
    {
        public static Log ServerStarted(IpPort adress)
        {
            return new Log("Server has started", adress.ToString(), Enums.Level.Success);
        }

        public static Log UserConnected(User user)
        {
            return new Log("User have connected",user.Adress.ToString(), Enums.Level.Informal);
        }

        public static Log AnonymousUserConnected(IpPort adress)
        {
            return new Log("Anonymous user have connected",adress.ToString() , Enums.Level.Informal);
        }

        public static Log UserDisconnected(User user)
        {
            return new Log("User have disconected", user.Adress.ToString(), Enums.Level.Informal);

        }

        public static Log UserNotFound(string? ipPort)
        {
            return new Log("User was not found", ipPort, Enums.Level.Warning);
        }

        public static Log UserNotFoundToSend(User user)
        {
            return new Log("User was not found connected to server to send message", user.UserName+" - "+user.Id , Enums.Level.ServerError);
        }

        public static Log ContextChange(EntityEntry entryChanged)
        {
            return new Log(entryChanged.State.ToString(), entryChanged.Entity.GetType().ToString(), Enums.Level.Informal);
        }

        public static Log StringMessageRecieved(User sender, string content)
        {
            return new Log(content, sender.UserName, Enums.Level.Informal);
        }

        public class Authentication
        {
            public static Log UserNotFoundByUsername(string username, string ipPort)
            {
                return new Log("User not found when authentication", username + " - " + ipPort);
            }

            public static Log WrongPassword(string username, string ipPort)
            {
                return new Log("User wrong Password", $"{username} - {ipPort}");
            }

            public static Log Authenticated(User user, string ipPort)
            {
                return new Log("User authenticated " + user.Id, $"{ipPort}");
            }

            public static Log AlreadyConnected(User user, string ipPort)
            {
                return new Log("User already connected " + user.Id + $" ({user.UserName})", $"{ipPort}");
            }
        }
    }
}
