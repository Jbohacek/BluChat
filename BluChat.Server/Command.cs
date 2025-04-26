using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.ServerFolder;

namespace BluChat.ServerConsole
{
    public abstract class Command(Server server)
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        private Server Server { get; set; } = server;

        public abstract void InvokeCommand(string[] inputs);
    }
}
