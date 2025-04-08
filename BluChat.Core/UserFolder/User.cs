using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using BluChat.Core;
using BluChat.Core.Data;
using BluChat.Core.Data.Interfaces;
using BluChat.Core.Messages.Data;
using BluChat.Core.UserFolder;


namespace BluChat.Core.UserFolder
{
    [Table("tbUsers")]
    public class User : ITable
    {
        [Key]public Guid Id { get; set; }

        [MaxLength(30)] public string UserName { get; set; } = null!;
        public string HashPassword { get; set; } = null!;
        [DefaultValue("Default")]public string ProfilePicPath { get; set; } = "Default";
        
        public List<Chat> Chats { get; set; } = new List<Chat>();
        [XmlIgnore]public List<Message> Messages { get; set; } = new List<Message>();

        [XmlIgnore][NotMapped]public UserServerStatus? ServerStatus { get; set; }
        [XmlIgnore][NotMapped]public IpPort? Adress => ServerStatus?.Adress;
        


        public User(IpPort adress, DateTime timeOfConnection)
        {
            ServerStatus = new UserServerStatus(adress, timeOfConnection);
        }

        public User()
        {
            
        }

        public User(string ipPort, DateTime timeOfConnection) : this(new IpPort(ipPort), timeOfConnection)
        {

        }


    }
}
