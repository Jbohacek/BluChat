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

        [NotMapped] public IpPort Adress { get; set; } = adress;
        [NotMapped] public DateTime TimeOfConnection { get; set; } = timeOfConnection;

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
