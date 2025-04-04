using BluChat.Core.Messages.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Data.Repositories
{
    public class ChatLogRepository(SqlLiteContext context) : Repo<MessageDataLog>(context)
    {
        private SqlLiteContext _context;

    }
}
