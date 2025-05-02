using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Common.Entities;
using BluChat.Core.Common.Interfaces;
using BluChat.Core.Data.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace BluChat.Core.Data.EF.Repositories
{
    public class UserRepository(SqlLiteContext context) : Repo<User>(context)
    {
        public override void Add(User item)
        {
            if (Exists(x => x.UserName.ToLower() == item.UserName.ToLower()))
            {
                throw new Exception($"User with username {item.UserName} already exists");
            }
            
            base.Add(item);
        }

        public User GetAdmin()
        {
            return GetFirst(x => x.UserName == "Admin");
        }

        public bool Exists(Func<User,bool> predicate)
        {
            return context.Users.Any(predicate);
        }
    }
}
