using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.UserFolder;

namespace BluChat.Core.Logger
{
    public static class LogFactory
    {
        public static Log ServerStarted()
        {
            return new Log("Server has started", Enums.Level.Success);
        }

        public static Log UserConnected(User user)
        {
            return new Log("User have connected",user.adress.ToString(), Enums.Level.Informal);
        }

        public static Log UserDisconnected(User user)
        {
            return new Log("User have disconected", user.adress.ToString(), Enums.Level.Informal);

        }

        public static Log UserNotFound(string ipPort)
        {
            return new Log("User was not found", ipPort, Enums.Level.Warning);
        }
    }
}
