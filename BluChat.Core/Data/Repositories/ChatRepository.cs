using BluChat.Core.Messages.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluChat.Core.Data.Repositories
{
    public class ChatRepository(SqlLiteContext context) : Repo<Chat>(context)
    {
        private readonly SqlLiteContext _context = context;

    }
}
