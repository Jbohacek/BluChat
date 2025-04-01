using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Logger.Interfaces
{
    public interface ILogSaver
    {
        public ILogger Logger { get; set; }
        public string Path { get; set; }
        public void Write(bool append = true);

    }
}
