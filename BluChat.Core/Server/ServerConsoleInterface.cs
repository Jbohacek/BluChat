using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Server
{
    public class ServerConsoleInterface(Server server)
    {
        private Server Server { get; set; } = server;

        private StringBuilder sb = new StringBuilder();

        private void AddTitle(string title)
        {
            sb.Append($"<--- {title} --->\n");
        }


        public string GetUsersList()
        {
            sb.Clear();
            AddTitle("User List");
            sb.Append("Guid\t\t\t\t\tadress\t\t\tTimeSpendOn\tTimeOfJoin\n");

            Server.ConnectedUsers.ForEach(x =>
            {
                sb.Append($"{x.Id}\t{x.Adress}\t\t{x.ServerStatus.TimeOnServerFormatted()}\t{x.ServerStatus.TimeOfConnection.ToUniversalTime()}\n");
            });

            return sb.ToString();
        }

    }
}
