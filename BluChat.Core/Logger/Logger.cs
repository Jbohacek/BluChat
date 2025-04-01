using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Logger.Interfaces;

namespace BluChat.Core.Logger
{
    public class Logger : ILogger
    {
        public ICollection<ILog> Logs { get; set; }

        public ILogSaver? LogSaver { get; set; }
        public ILogLoader? LogLoader { get; set; }


        public List<ILog> ToList() => Logs.ToList();
        public BindingList<ILog> GetBindingList() => new BindingList<ILog>(ToList());
        public int GetCount() => Logs.Count;

        public Logger(ILogSaver saver)
        {
            Logs = new List<ILog>();
            LogSaver = saver;
        }

        public Logger(ILogLoader load)
        {
            Logs = new List<ILog>();
            LogLoader = load;
        }

        public Logger(ILogSaver saver, ILogLoader loader)
        {
            Logs = new List<ILog>();
            LogSaver = saver;
            LogLoader = loader;
        }

        private void Load()
        {
            throw new NotImplementedException();

        }
        private void Save() => LogSaver?.Write();


        public void Add(ILog log)
        {
            Logs.Add(log);
            LogSaver?.Write();
            OnLogAdded(log);
        }


        public void Remove(ILog log) => Logs.Remove(log); 
        public void Clear() => Logs.Clear();


        public event EventHandler<LogEventHandler>? LogAdded;
        public void OnLogAdded(ILog log)
        {
            LogAdded?.Invoke(this,new LogEventHandler(log));
        }

    }
}
