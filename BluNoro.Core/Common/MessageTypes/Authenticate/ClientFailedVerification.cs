using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Client.Infrastructure;
using BluNoro.Core.Messages.Abstracts;


namespace BluNoro.Core.Common.MessageTypes.Authenticate
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
