using BluChat.Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Messages.Data
{
    [Table("tbChatMessages")]
    public class MessageDataLog : ITable
    {
        [Key] public Guid Id { get; set; }
        public MessageChat ParentChat { get; set; } = null!;
        string UnformatedMessage { get; set; } = null!;

    }
}
