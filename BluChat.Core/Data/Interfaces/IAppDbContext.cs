using BluChat.Core.UserFolder;
using Microsoft.EntityFrameworkCore;

namespace BluChat.Core.Data.Interfaces;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
}