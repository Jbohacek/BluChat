using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Messages.Abstracts;
using BluChat.Core.Messages.Data;

namespace BluChat.Core.Messages.MessageTypes.Authenticate
{
    public class ClientFailedVerification : MessageBaseServer
    {
        public string Reason { get; set; }
        
        public override void MessangeHandler(MessageServerManager serverManager)
        {
            throw new NotImplementedException();
        }

        public override Message Convert()
        {
            throw new NotImplementedException();
        }
    }
}
