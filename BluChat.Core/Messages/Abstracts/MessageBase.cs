using BluChat.Core.Messages.MessageTypes;
using BluChat.Core.Messages.SenderReciever;
using BluChat.Core.ServerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BluChat.Core.Messages.Data;

namespace BluChat.Core.Messages.Abstracts
{
    [XmlInclude(typeof(StringMessageBase))]
    public abstract class MessageBase
    {
        [XmlIgnore] Server Server { get; set; }

        public SenderUser Sender { get; set; }
        public Chat ParentChat { get; set; } = null!;

        public DateTime SendTime { get; set; } = DateTime.Now;

        public abstract void MessangeHandler(MessageManager manager);

        public abstract Message Convert();




    }
}
