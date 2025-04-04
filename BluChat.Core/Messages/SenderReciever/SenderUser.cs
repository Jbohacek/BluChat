using BluChat.Core.Messages.Abstracts;
using BluChat.Core.UserFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BluChat.Core.Messages.SenderReciever
{
    public class SenderUser : MessageUser
    {

        public SenderUser(User user) : base(user)
        {
        }

        public SenderUser() : base(new User())
        {
            
        }

    }
}
