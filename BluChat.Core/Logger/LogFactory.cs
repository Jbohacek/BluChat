using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Logger
{
    public static class LogFactory
    {
        public static Log ServerStarted()
        {
            return new Log("Server has started", Enums.Level.Success);
        }
    }
}
