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

            Server.Users.ForEach(x =>
            {
                sb.Append($"{x.Id}\t{x.adress}\t\t{x.TimeOnServerFormatted()}\t{x.TimeOfConnection.ToUniversalTime()}\n");
            });

            return sb.ToString();
        }

    }
}
