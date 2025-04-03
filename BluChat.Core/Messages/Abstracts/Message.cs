using BluChat.Core.Messages.MessageTypes;
using BluChat.Core.Messages.SenderReciever;
using BluChat.Core.ServerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BluChat.Core.Messages.Abstracts
{
    [XmlInclude(typeof(StringMessage))]
    public abstract class Message
    {
        [XmlIgnore] Server Server { get; set; }

        [XmlIgnore]  public SenderUser Sender { get; set; }
        [XmlIgnore]  public IEnumerable<RecieverUser> Reciever { get; set; } = new List<RecieverUser>();

        public DateTime SendTime { get; set; } = DateTime.Now;

        public abstract void MessangeHandler();


    }
}
