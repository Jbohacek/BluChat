using BluChat.Core.Messages.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Data.Repositories
{
    class ChatMessageRepository : Repo<MessageDataLog>
    {
        private readonly SqlLiteContext _context;

        public ChatMessageRepository(SqlLiteContext context) : base(context)
        {
            _context = context;
        }
    }
}
