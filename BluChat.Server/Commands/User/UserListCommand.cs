using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.ServerFolder;

namespace BluChat.ServerConsole.Commands.User
{
    public class UserListCommand(Server server) : Command(server)
    {
        public override string Name => "UserList";

        public override string Description => "This will list of every user connected to server";

        public override void InvokeCommand(string[] inputs)
        {
            //todo: dodělat userlistadd

            throw new Exception("dodělat");
        }
    }
}
