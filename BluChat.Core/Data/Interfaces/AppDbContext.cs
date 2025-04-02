using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.UserFolder;
using Microsoft.EntityFrameworkCore;

namespace BluChat.Core.Data.Interfaces
{
    public abstract class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<User> Users { get; set; }

        protected AppDbContext()
        {
        }
    }
}
