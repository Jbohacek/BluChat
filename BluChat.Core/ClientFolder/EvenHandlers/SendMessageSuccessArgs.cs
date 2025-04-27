using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Messages.Data;

namespace BluChat.Core.ClientFolder.EvenHandlers
{
    public class SendMessageSuccessArgs(Message message) : EventArgs
    {
        public Message Message { get; set; } = message;

    }
}
