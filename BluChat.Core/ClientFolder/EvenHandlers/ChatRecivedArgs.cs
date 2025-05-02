using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Common.Entities;

namespace BluChat.Core.ClientFolder.EvenHandlers
{
    public class ChatRecivedArgs : EventArgs
    {
        public List<Chat> Chats;
    }
}
