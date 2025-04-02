using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Data.Interfaces;
using BluChat.Core.UserFolder;

namespace BluChat.Core.Data.Repositories
{
    public class UserRepository(AppDbContext context) : Repo<User>(context)
    {
        private readonly AppDbContext _context = context;

        public override void Add(User item)
        {
            item.HashPassword = PasswordManager.HashPassword(item.HashPassword);
            base.Add(item);
        }
    }
}
