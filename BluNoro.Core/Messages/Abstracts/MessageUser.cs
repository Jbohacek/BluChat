using BluNoro.Core.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BluNoro.Core.Messages.Abstracts
{
    
    public abstract class MessageUser(User user)
    {
        public string IpPort { get; set; }
        public User? User { get; set; }

        public void FindUser(MessageServerManager serverManager)
        {
            User = serverManager.Database.Users.GetFirst(x => x.Id == User.Id);
        }
    }
}
