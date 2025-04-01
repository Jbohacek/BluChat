namespace BluChat.Core.UserFolder
{
    public class User
    {
        public User(IpPort adress)
        {
            adress = adress;
        }

        public User(string ipPort)
        {
            adress = new IpPort(ipPort);
        }

        public IpPort adress { get; set; }



    }
}
