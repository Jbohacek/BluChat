using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.UserFolder
{
    public class UserServerStatus(IpPort adress, DateTime timeOfConnection)
    {
        public UserServerStatus(string ipPort, DateTime timeOfConnection) : this(new IpPort(ipPort), timeOfConnection)
        {

        }

        public IpPort Adress { get; set; } = adress;
        public DateTime TimeOfConnection { get; set; } = timeOfConnection;
        public bool IsConnected { get; set; } = false;

        public TimeSpan TimeOnServer()
        {
            return DateTime.Now - TimeOfConnection;
        }

        public string TimeOnServerFormatted()
        {
            return TimeOnServer().ToString(@"hh\:mm\:ss");
        }
    }
}
