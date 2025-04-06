using BluChat.Core.UserFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BluChat.Core.Messages.Abstracts
{
    
    public abstract class MessageUser(User user)
    {
        public Guid Id { get; set; } = user.Id;
        public string IpPort { get; set; }


        [XmlIgnore]public User User { get; set; }

        public void FindUser(MessageServerManager serverManager)
        {
            User = serverManager.Database.Users.GetFirstOrDefault(x => x.Id == Id);
        }
    }
}
