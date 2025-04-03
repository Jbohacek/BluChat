using BluChat.Core.UserFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Messages.Abstracts
{
    public abstract class MessageUser(User user)
    {
        public IpPort Adress => User.Adress;
        public User User { get; set; }

    }
}
