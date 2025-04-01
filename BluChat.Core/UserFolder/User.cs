namespace BluChat.Core.UserFolder
{
    public class User
    {
        public Guid Id { get; set; }

        public User(IpPort adress)
        {
            this.adress = adress;
            Id = Guid.NewGuid();
            TimeOfConnection = DateTime.Now;
        }

        public User(string ipPort) : this(new IpPort(ipPort))
        {

        }

        public IpPort adress { get; set; }
        public DateTime TimeOfConnection { get; set; }

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
