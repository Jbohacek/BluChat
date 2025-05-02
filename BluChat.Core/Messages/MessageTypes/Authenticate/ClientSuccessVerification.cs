using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Common.Entities;
using BluChat.Core.Messages.Abstracts;

namespace BluChat.Core.Messages.MessageTypes.Authenticate
{
    public class ClientSuccessVerification : MessageBaseClient
    {
        public User authenticatedUser { get; set; }

        public override void MessangeHandler(MessageClientManager clientManager)
        {
            clientManager.Client.Sender.User = authenticatedUser;
            clientManager.Client.Events.OnUserVerificion(this);
        }
    }
}
