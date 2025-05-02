using BluNoro.Core.Messages.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluNoro.Core.Messages.MessageTypes
{
    public class ClientUniversalBroadcastMessage : MessageBaseClient
    {
        public override void MessangeHandler(MessageClientManager clientManager)
        {
            throw new NotImplementedException();
        }
    }
}
