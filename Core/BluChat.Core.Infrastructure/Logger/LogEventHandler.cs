using BluChat.Core.Infrastructure.Logger.Interfaces;


namespace BluChat.Core.Infrastructure.Logger
{
    public class LogEventHandler(ILog log) : EventArgs
    {
        public ILog Log { get; set; } = log;
        public DateTime Time = DateTime.Now;
    }
}
