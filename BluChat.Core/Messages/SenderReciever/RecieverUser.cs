using BluChat.Core.Common.Entities;
using BluChat.Core.Messages.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Messages.SenderReciever
{
    public class RecieverUser : MessageUser
    {
        public RecieverUser(User user) : base(user)
        {
            
        }

        public RecieverUser() : base(new User())
        {
            
        }
    }
}
