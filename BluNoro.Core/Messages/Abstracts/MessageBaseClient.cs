using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Messages.SenderReciever;

namespace BluNoro.Core.Messages.Abstracts
{
    public abstract class MessageBaseClient
    {
        public RecieverUser RecieverUser { get; set; }

        public DateTime SendTime { get; set; } = DateTime.Now;

        public abstract void MessangeHandler(MessageClientManager clientManager);

        public string SerlizeMe(MessageSerializer serializer)
        {
            return serializer.SerializeMessageToString(this);
        }

    }
}
