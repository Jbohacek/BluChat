using BluChat.Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.UserFolder;

namespace BluChat.Core.Messages.Data
{
    [Table("tbChatMessages")]
    public class Message : ITable
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        public Chat ParentChat { get; set; } = null!;
        public string UnformatedMessage { get; set; } = null!;
        public User Sender { get; set; } = null!;

        public override string ToString()
        {
            return Sender.UserName + ": " +  UnformatedMessage;
        }
    }
}
