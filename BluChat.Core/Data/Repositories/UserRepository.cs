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
            if (Exists(x => x.UserName.ToLower() == item.UserName.ToLower()))
            {
                throw new Exception($"User with username {item.UserName} already exists");
            }

            item.HashPassword = PasswordManager.HashPassword(item.HashPassword);
            base.Add(item);
        }

        public User GetAdmin()
        {
            return this.GetFirst(x => x.UserName == "Admin");
        }

        public void UpdatePassword(string username, string password)
        {
            User? user = _context.Users.FirstOrDefault(x => x.UserName == username);
            if (user == null)
                throw new Exception($"{username} does not exists");

            user.HashPassword = PasswordManager.HashPassword(password);
            base.Update(user);
        }

        public bool Exists(Func<User,bool> predicate)
        {
            return _context.Users.Any(predicate);
        }
    }
}
