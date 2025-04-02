using BluChat.Core.Data.Interfaces;
using BluChat.Core.UserFolder;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace BluChat.Core.Data;

public class SqlLiteContext : DbContext, IAppDbContext
{
    private string FileName { get; set; }

    public SqlLiteContext()
    {
        Database.EnsureCreated();
    }

    public SqlLiteContext(string fileName) : base()
    {
        FileName = fileName;
        Database.EnsureCreated();

        Batteries.Init();

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=" + AppDomain.CurrentDomain.BaseDirectory + FileName + ".db");
    }

    public DbSet<User> Users { get; set; }

}
