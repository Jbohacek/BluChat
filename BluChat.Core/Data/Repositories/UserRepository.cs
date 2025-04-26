using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Data.Interfaces;
using BluChat.Core.UserFolder;

namespace BluChat.Core.Data.Repositories
{
    public class UserRepository(SqlLiteContext context) : Repo<User>(context)
    {
        private readonly SqlLiteContext _context = context;

        public override void Add(User item)
        {
            if (Exists(x => x.UserName == item.UserName))
            {
                throw new Exception($"User with username {item.UserName} already exists");
            }

            item.HashPassword = PasswordManager.HashPassword(item.HashPassword);
            base.Add(item);
        }

        public User GetAdmin()
        {
            return this.GetFirstOrDefault(x => x.UserName == "Admin");
        }

        public bool Exists(Func<User,bool> predicate)
        {
            return _context.Users.Any(predicate);
        }
    }
}
