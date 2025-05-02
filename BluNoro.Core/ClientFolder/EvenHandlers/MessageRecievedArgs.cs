using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Entities;

namespace BluNoro.Core.ClientFolder.EvenHandlers
{
    public class MessageRecievedArgs : EventArgs
    {
        public Message Message { get; set; }
        public Guid ChatId { get; set; }
    }
}
