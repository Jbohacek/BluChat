using BluChat.Core.Messages.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Messages.MessageTypes
{
    public class StringMessage : Message
    {
        public string Content { get; set; } = "Test";
        public override void MessangeHandler()
        {
            
        }
    }
}
