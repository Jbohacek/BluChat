using BluChat.Core.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Logger
{
    public class LogEventHandler(ILog log) : EventArgs
    {
        public ILog Log { get; set; } = log;
        public DateTime Time = DateTime.Now;
    }
}
