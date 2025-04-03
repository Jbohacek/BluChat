using BluChat.Core.Data.Interfaces;
using BluChat.Core.UserFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Messages.Data
{
    [Table("tbChats")]
    public class MessageChat : ITable
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();

        public IEnumerable<User> Users { get; set; } = new List<User>();
        public IEnumerable<MessageDataLog> Messages { get; set; } = new List<MessageDataLog>();

        public DateTime CreationOfCreation { get; set; } = DateTime.Now;

        public DateTime LastTimeEdited { get; set; } = DateTime.Now;




    }
}
