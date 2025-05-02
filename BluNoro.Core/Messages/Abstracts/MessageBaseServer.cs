using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Messages.MessageTypes;
using BluNoro.Core.Messages.SenderReciever;
using BluNoro.Core.Networking;

namespace BluNoro.Core.Messages.Abstracts
{
    [XmlInclude(typeof(StringMessage))]
    public abstract class MessageBaseServer
    {
        [XmlIgnore] Server Server { get; set; }

        public SenderUser Sender { get; set; }
        public Chat ParentChat { get; set; } = null!;

        public bool ToSave = false;

        public DateTime SendTime { get; set; } = DateTime.Now;

        public abstract void MessangeHandler(MessageServerManager serverManager);

        public abstract Message Convert();

        public string SerlizeMe(MessageSerializer serializer)
        {
            return serializer.SerializeMessageToString(this);
        }

    }
}
