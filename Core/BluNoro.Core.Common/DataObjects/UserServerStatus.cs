namespace BluNoro.Core.Common.DataObjects
{
    public class UserServerStatus(IpPort adress, DateTime timeOfConnection)
    {
        public UserServerStatus(string ipPort, DateTime timeOfConnection) : this(new IpPort(ipPort), timeOfConnection)
        {

        }

        public IpPort Adress { get; set; } = adress;
        public DateTime TimeOfConnection { get; set; } = timeOfConnection;
        public bool IsConnected { get; set; } = false;

        public TimeSpan TimeOnServer()
        {
            return DateTime.Now - TimeOfConnection;
        }

        public string TimeOnServerFormatted()
        {
            return TimeOnServer().ToString(@"hh\:mm\:ss");
        }
    }
}
