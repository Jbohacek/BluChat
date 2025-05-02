using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Common.Entities;

namespace BluChat.Core.ClientFolder.EvenHandlers
{
    public class GetChatMessagesEventHandler : EventArgs
    {
        public List<Message> Messages { get; set; }
    }
}
