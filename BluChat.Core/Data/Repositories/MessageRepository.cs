using BluChat.Core.Messages.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Data.Repositories
{
    public class MessageRepository(SqlLiteContext context) : Repo<Message>(context)
    {
        private SqlLiteContext _context = context;

        public override void Add(Message item)
        {
            item.ParentChat = _context.Chats.First(x => x.Id == item.ParentChat.Id);
            item.Sender = _context.Users.First(x => x.Id == item.Sender.Id);

            _context.Messages.Add(item);
        }
    }
}
