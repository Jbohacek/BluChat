using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using BluChat.Core;
using BluChat.Core.Data;
using BluChat.Core.Data.Interfaces;
using BluChat.Core.UserFolder;


namespace BluChat.Core.UserFolder
{
    [Table("tbUsers")]
    public class User : ITable
    {
        [Key]public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(30)] public string UserName { get; set; } = null!;
        public string HashPassword { get; set; } = null!;
        [DefaultValue("Default")]public string ProfilePicPath { get; set; } = "Default";
        


        [NotMapped]public UserServerStatus? ServerStatus { get; set; }
        [NotMapped]public IpPort? Adress => ServerStatus?.Adress;
        


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
