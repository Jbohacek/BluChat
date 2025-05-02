using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Messages.Abstracts;


namespace BluChat.Core.Messages.MessageTypes.Authenticate
{
    public class ClientFailedVerification : MessageBaseClient
    {
        public string Reason { get; set; }
        
        public override void MessangeHandler(MessageClientManager clientManager)
        {
            clientManager.Client.Events.OnUserFailedVerification(this);
        }

    }
}
