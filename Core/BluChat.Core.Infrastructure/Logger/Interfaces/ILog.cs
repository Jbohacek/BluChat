using BluChat.Core.Contracts.Enums;

namespace BluChat.Core.Infrastructure.Logger.Interfaces
{
    public interface ILog
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Enums.Level Level { get; set; }

        public string GetLog();

    }
}