using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Logger.Interfaces
{
    public interface ILogLoader
    {
        public string Path { get; set; }
        public ICollection<ILog> Load();
    }
}
