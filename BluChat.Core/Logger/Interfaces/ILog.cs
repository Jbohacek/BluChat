using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Logger
{
    public interface ILog
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Enums.Level Level { get; set; }

        public string GetLog();

    }
}
