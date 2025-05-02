using BluNoro.Core.Common.Entities;
using BluNoro.Core.Messages.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BluNoro.Core.Messages.SenderReciever
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
