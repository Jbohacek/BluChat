using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Contracts.Interfaces;

namespace BluNoro.Core.Common.Entities
{
    [Table("tbUsers")]
    public class User : ITable
    {
        [Key]public Guid Id { get; set; }

        [MaxLength(30)] public string UserName { get; set; } = null!;
        [XmlIgnore]public string HashPassword { get; set; } = null!;
        [DefaultValue("Default")]public string ProfilePicPath { get; set; } = "Default";
        
        [XmlIgnore]public List<Chat> Chats { get; set; } = new List<Chat>();
        [XmlIgnore]public List<Message> Messages { get; set; } = new List<Message>();

        [XmlIgnore][NotMapped]public ConnectionStatus? ServerStatus { get; set; }
        [XmlIgnore][NotMapped]public IpPort? Adress => ServerStatus?.Adress;
        


        public User(IpPort adress, DateTime timeOfConnection)
        {
            ServerStatus = new ConnectionStatus(adress, timeOfConnection);
        }

        public User()
        {
            
        }

        public User(string ipPort, DateTime timeOfConnection) : this(new IpPort(ipPort), timeOfConnection)
        {

        }


    }
}
