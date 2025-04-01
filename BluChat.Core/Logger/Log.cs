using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Logger.Interfaces;

namespace BluChat.Core.Logger
{
    public class Log : ILog
    {
        public Log(string name,string message = "", Enums.Level level = Enums.Level.Informal)
        {
            CreateLog(name,message, DateTime.Now,level );
        }
        public Log(string name, Enums.Level level = Enums.Level.Informal, string message = "")
        {
            CreateLog(name, message, DateTime.Now, level);
        }

        private void CreateLog(string name ,string message, DateTime time,Enums.Level level)
        {
            Name = name;
            Content = message;
            Date = time;
            Id = Guid.NewGuid();
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public Enums.Level Level { get; set; }

        public string GetLog()
        {
            return $"[{Date:yyyy-MM-dd HH:mm:ss}] [{Level.ToString()}] - {Name} ({Content})";
        }

        public override string ToString()
        {
            return GetLog();
        }
    }
}
