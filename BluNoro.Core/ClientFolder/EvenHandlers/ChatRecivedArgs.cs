using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Entities;

namespace BluNoro.Core.ClientFolder.EvenHandlers
{
    public class ChatRecivedArgs : EventArgs
    {
        public List<Chat> Chats;
    }
}
