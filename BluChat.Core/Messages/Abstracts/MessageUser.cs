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

        [XmlIgnore]public IpPort Adress => User.Adress;
        [XmlIgnore]public User User { get; set; }

        public void FindUser(MessageManager manager)
        {
            User = manager.Database.Users.GetFirstOrDefault(x => x.Id == Id);
        }
    }
}
